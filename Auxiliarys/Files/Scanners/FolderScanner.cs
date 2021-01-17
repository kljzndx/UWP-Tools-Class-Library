using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys.Files.Scanners
{
    public class FolderScanner
    {
        private QueryOptions _options;

        public FolderScanner()
        {
            _options = new QueryOptions(CommonFolderQuery.DefaultQuery);
            _options.FolderDepth = FolderDepth.Deep;
        }

        public QueryOptions Options => _options;

        public async Task ScanByFolderQuery(IEnumerable<StorageFolder> folders, Func<IEnumerable<StorageFolder>, Task> callback, uint maxCountInOneList = 10)
        {
            foreach (var folder in folders)
                await ScanByFolderQuery(folder, callback, maxCountInOneList);
        }
        
        public async Task ScanByFolderQuery(StorageFolder folder, Func<IEnumerable<StorageFolder>, Task> callback, uint maxCountInOneList = 10)
        {
            await callback.Invoke(new[] { folder });

            uint id = 0;
            var queryResult = folder.CreateFolderQueryWithOptions(_options);
            uint count = await queryResult.GetItemCountAsync();
            while (id < count)
            {
                await callback.Invoke(await queryResult.GetFoldersAsync(id, maxCountInOneList));
                id += maxCountInOneList;
            }
        }

        public async Task ScanByRecursion(IEnumerable<StorageFolder> folders, Func<StorageFolder, Task> callback)
        {
            foreach (var folder in folders)
                await ScanByRecursion(folder, callback);
        }
        
        public async Task ScanByRecursion(StorageFolder folder, Func<StorageFolder, Task> callback)
        {
            await callback.Invoke(folder);
            
            var folders = await folder.GetFoldersAsync();
            foreach (var item in folders)
                await ScanByRecursion(item, callback);
        }
    }
}