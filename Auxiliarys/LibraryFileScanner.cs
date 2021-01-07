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

        public async Task ScanByChangeTracker(Func<IEnumerable<StorageLibraryChange>, Task> callback, uint maxCountInOneList = 10)
        {
            _library.ChangeTracker.Enable();
            var reader =  _library.ChangeTracker.GetChangeReader();
            var allFileChanges = (await reader.ReadBatchAsync()).Where(c => c.IsOfType(StorageItemTypes.File));
            var changes = new Queue<StorageLibraryChange>(
                allFileChanges.Where(c =>
                    _options.FileTypeFilter.Any(s => s.Replace(".", String.Empty) == c.Path.Split('.').LastOrDefault()))
            );

            if (changes.Any(c => c.ChangeType == StorageLibraryChangeType.ChangeTrackingLost))
            {
                _library.ChangeTracker.Reset();
                return;
            }

            while (changes.Any())
            {
                var tempChanges = new List<StorageLibraryChange>();
                for (int i = 0; i < maxCountInOneList; i++)
                {
                    if (!changes.Any())
                        break;

                    tempChanges.Add(changes.Dequeue());
                }

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