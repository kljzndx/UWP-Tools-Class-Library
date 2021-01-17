using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys.Files.Scanners
{
    public class LibraryFileScanner
    {
        private StorageLibrary _library;
        private FileScanner _fileScanner;

        public LibraryFileScanner(StorageLibrary library, params string[] extensionNames)
        {
            _library = library;
            _fileScanner = new FileScanner(extensionNames);
        }

        public QueryOptions Options => _fileScanner.Options;

        public async Task ScanByChangeTracker(Func<IEnumerable<KeyValuePair<StorageItemTypes, StorageLibraryChange>>, Task> callback, uint maxCountInOneList = 10)
        {
            _library.ChangeTracker.Enable();
            var reader =  _library.ChangeTracker.GetChangeReader();
            var allFileChanges = await reader.ReadBatchAsync();
            var changes = new Queue<StorageLibraryChange>(
                allFileChanges.Where(c =>
                    !c.IsOfType(StorageItemTypes.File) ||
                    c.IsOfType(StorageItemTypes.File) && _fileScanner.CheckExtensionName(c.Path))
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

        public Task ScanByFileQuery(Func<IEnumerable<StorageFile>, Task> callback, uint maxCountInOneList = 10)
            => _fileScanner.ScanByFileQuery(_library.Folders, callback, maxCountInOneList);

        public Task ScanByRecursion(Func<IEnumerable<StorageFile>, Task> callback, int maxCountInOneList = 10)
            => _fileScanner.ScanByRecursion(_library.Folders, callback, maxCountInOneList);
    }
}