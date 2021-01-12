using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys
{
    public class LibraryFileScanner
    {
        private StorageLibrary _library;
        private QueryOptions _options;

        public LibraryFileScanner(StorageLibrary library, params string[] extensionNames)
        {
            _library = library;
            _options = new QueryOptions(CommonFileQuery.OrderByName, extensionNames.Select(s => s.Trim())
                .Select(en => en.FirstOrDefault() != '.' ? "." + en : en));

            _options.FolderDepth = FolderDepth.Deep;
        }

        public LibraryFileScanner(StorageLibrary library, IndexerOption indexerOption, params string[] extensionNames) : this(library, extensionNames)
        {
            _options.IndexerOption = indexerOption;
        }

        public LibraryFileScanner(StorageLibrary library, QueryOptions queryOptions)
        {
            _library = library;
            _options = queryOptions;
        }

        public async Task ScanByChangeTracker(Func<IEnumerable<KeyValuePair<StorageItemTypes, StorageLibraryChange>>, Task> callback, uint maxCountInOneList = 10)
        {
            _library.ChangeTracker.Enable();
            var reader =  _library.ChangeTracker.GetChangeReader();
            var allFileChanges = await reader.ReadBatchAsync();
            var changes = new Queue<StorageLibraryChange>(
                allFileChanges.Where(c =>
                    !c.IsOfType(StorageItemTypes.File) ||
                    c.IsOfType(StorageItemTypes.File) &&
                    _options.FileTypeFilter.Any(s => s.Replace(".", String.Empty) == c.Path.Split('.').LastOrDefault()))
            );

            if (changes.Any(c => c.ChangeType == StorageLibraryChangeType.ChangeTrackingLost))
            {
                _library.ChangeTracker.Reset();
                return;
            }

            while (changes.Any())
            {
                var tempChanges = new List<KeyValuePair<StorageItemTypes, StorageLibraryChange>>();
                for (int i = 0; i < maxCountInOneList; i++)
                {
                    if (!changes.Any())
                        break;

                    var change = changes.Dequeue();

                    if (change.IsOfType(StorageItemTypes.File))
                        tempChanges.Add(new KeyValuePair<StorageItemTypes, StorageLibraryChange>(StorageItemTypes.File, change));
                    else if (change.IsOfType(StorageItemTypes.Folder))
                        tempChanges.Add(new KeyValuePair<StorageItemTypes, StorageLibraryChange>(StorageItemTypes.Folder, change));
                    else
                        tempChanges.Add(new KeyValuePair<StorageItemTypes, StorageLibraryChange>(StorageItemTypes.None, change));
                }

                if (tempChanges.Any())
                    await callback.Invoke(tempChanges);
            }

            await reader.AcceptChangesAsync();
        }

        public async Task ScanByFolder(Func<IEnumerable<StorageFile>, Task> callback, uint maxCountInOneList = 10)
        {
            foreach (var libraryFolder in _library.Folders)
            {
                uint id = 0;
                var queryResult = libraryFolder.CreateFileQueryWithOptions(_options);
                uint count = await queryResult.GetItemCountAsync();
                while (id < count)
                {
                    await callback.Invoke(await queryResult.GetFilesAsync(id, maxCountInOneList));
                    id += maxCountInOneList;
                }
            }
        }
    }
}