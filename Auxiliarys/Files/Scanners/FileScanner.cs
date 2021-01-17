using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys.Files.Scanners
{
    public class FileScanner
    {
        private string[] _extensionNames;
        private QueryOptions _options;
        private FolderScanner _folderScanner;

        public FileScanner(params string[] extensionNames)
        {
            _extensionNames = extensionNames.Select(s => s.Trim())
                .Select(en => en.FirstOrDefault() == '.' ? en.Remove(0, 1) : en).ToArray();
            _options = new QueryOptions(CommonFileQuery.OrderByName,
                _extensionNames.Select(s => "." + s).ToArray());

            _options.FolderDepth = FolderDepth.Deep;
        }

        public QueryOptions Options => _options;

        public async Task ScanByFileQuery(IEnumerable<StorageFolder> folders, Func<IEnumerable<StorageFile>, Task> callback, uint maxCountOfItem = 10)
        {
            foreach (var folder in folders)
                await ScanByFileQuery(folder, callback, maxCountOfItem);
        }
        
        public async Task ScanByFileQuery(StorageFolder folder, Func<IEnumerable<StorageFile>, Task> callback, uint maxCountOfItem = 10)
        {
            uint id = 0;
            var queryResult = folder.CreateFileQueryWithOptions(_options);
            uint count = await queryResult.GetItemCountAsync();
            while (id < count)
            {
                await callback.Invoke(await queryResult.GetFilesAsync(id, maxCountOfItem));
                id += maxCountOfItem;
            }
        }

        public Task ScanByRecursion(StorageFolder folder, Func<IEnumerable<StorageFile>, Task> callback, int maxCountOfItem = 10)
            => ScanByRecursion(new[] { folder }, callback, maxCountOfItem);

        public async Task ScanByRecursion(IEnumerable<StorageFolder> folders, Func<IEnumerable<StorageFile>, Task> callback, int maxCountOfItem = 10)
        {
            if (_folderScanner == null)
                _folderScanner = new FolderScanner();

            await _folderScanner.ScanByRecursion(folders, async fd =>
            {
                var files = (await fd.GetFilesAsync()).Where(f => CheckExtensionName(f.Path)).ToList();
                int id = 0;

                while (id < files.Count)
                {
                    var items = files.Skip(id).Take(maxCountOfItem).ToList();
                    await callback.Invoke(items);
                    id += maxCountOfItem;
                }
            });
        }
        
        internal bool CheckExtensionName(string path)
        {
            return _extensionNames.Any(s => s == "*" || s == path.Split('.').LastOrDefault());
        }
    }
}