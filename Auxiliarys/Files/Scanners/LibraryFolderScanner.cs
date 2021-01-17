using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys.Files.Scanners
{
    public class LibraryFolderScanner
    {
        private StorageLibrary _library;
        private FolderScanner _folderScanner;
        
        public LibraryFolderScanner(StorageLibrary library)
        {
            _library = library;
            _folderScanner = new FolderScanner();
        }

        public QueryOptions Options => _folderScanner.Options;

        public Task ScanByFolderQuery(Func<IEnumerable<StorageFolder>, Task> callback, uint maxCountInOneList = 10)
            => _folderScanner.ScanByFolderQuery(_library.Folders, callback, maxCountInOneList);

        public Task ScanByRecursion(Func<StorageFolder, Task> callback)
            => _folderScanner.ScanByRecursion(_library.Folders, callback);
    }
}