using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys
{
    public class LibraryFolderScanner
    {
        private StorageLibrary _library;
        private QueryOptions _options;

        public LibraryFolderScanner(StorageLibrary library)
        {
            _library = library;
            _options = new QueryOptions(CommonFolderQuery.DefaultQuery);
            _options.FolderDepth = FolderDepth.Deep;
        }

        public QueryOptions Options => _options;

        public async Task ScanByFolderQuery(Func<IEnumerable<StorageFolder>, Task> callback, uint maxCountInOneList = 10)
        {
            foreach (var libraryFolder in _library.Folders)
            {
                await callback.Invoke(new[] { libraryFolder });

                uint id = 0;
                var queryResult = libraryFolder.CreateFolderQueryWithOptions(_options);
                uint count = await queryResult.GetItemCountAsync();
                while (id < count)
                {
                    await callback.Invoke(await queryResult.GetFoldersAsync(id, maxCountInOneList));
                    id += maxCountInOneList;
                }
            }
        }

        protected virtual async Task ScanByRecursion(StorageFolder folder, Func<StorageFolder, Task> callback)
        {
            var folders = await folder.GetFoldersAsync();
            foreach (var item in folders)
            {
                await callback.Invoke(item);
                await ScanByRecursion(item, callback);
            }
        }

        public async Task ScanByRecursion(Func<StorageFolder, Task> callback)
        {
            foreach (var libraryFolder in _library.Folders)
            {
                await callback.Invoke(libraryFolder);
                await ScanByRecursion(libraryFolder, callback);
            }
        }
    }
}