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
        private string[] _extensionNames;
        private QueryOptions _options;

        public LibraryFileScanner(StorageLibrary library, params string[] extensionNames)
        {
            _library = library;
            _extensionNames = extensionNames.Select(s => s.Trim())
                .Select(en => en.FirstOrDefault() == '.' ? en.Remove(0, 1) : en).ToArray();
            _options = new QueryOptions(CommonFileQuery.OrderByName,
                _extensionNames.Select(s => "." + s).ToArray());

            _options.FolderDepth = FolderDepth.Deep;
        }

        public QueryOptions Options => _options;

        public async Task ScanByChangeTracker(Func<IEnumerable<KeyValuePair<StorageItemTypes, StorageLibraryChange>>, Task> callback, uint maxCountInOneList = 10)
        {
            _library.ChangeTracker.Enable();
            var reader =  _library.ChangeTracker.GetChangeReader();
            var allFileChanges = await reader.ReadBatchAsync();
            var changes = new Queue<StorageLibraryChange>(
                allFileChanges.Where(c =>
                    !c.IsOfType(StorageItemTypes.File) ||
                    c.IsOfType(StorageItemTypes.File) && CheckExtensionName(c.Path))
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

        [Obsolete("Please use ScanByFileQuery method")]
        public Task ScanByFolder(Func<IEnumerable<StorageFile>, Task> callback, uint maxCountInOneList = 10)
            => ScanByFileQuery(callback, maxCountInOneList);

        public async Task ScanByFileQuery(Func<IEnumerable<StorageFile>, Task> callback, uint maxCountInOneList = 10)
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

        public async Task ScanByRecursion(Func<IEnumerable<StorageFile>, Task> callback, int maxCountInOneList = 10)
        {
            var fc = new LibraryFolderScanner(_library);

            await fc.ScanByRecursion(async folder =>
            {
                var files = (await folder.GetFilesAsync()).Where(f => CheckExtensionName(f.Path)).ToList();
                int id = 0;

                while (id < files.Count)
                {
                    var items = files.Skip(id).Take(maxCountInOneList).ToList();
                    await callback.Invoke(items);
                    id += maxCountInOneList;
                }
            });
        }

        private bool CheckExtensionName(string path)
        {
            return _extensionNames.Any(s => s == ".*" || s == path.Split('.').LastOrDefault());
        }
    }
}