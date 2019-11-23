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

        public async Task ScanByChangeTracker(Func<IEnumerable<StorageLibraryChange>, Task> callback)
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
                for (int i = 0; i < 100; i++)
                {
                    if (!changes.Any())
                        break;

                    tempChanges.Add(changes.Dequeue());
                }

                await callback.Invoke(tempChanges);
            }

            await reader.AcceptChangesAsync();
        }

        public async Task ScanByFolder(Func<IEnumerable<StorageFile>, Task> callback)
        {
            foreach (var libraryFolder in _library.Folders)
            {
                var queryResult = libraryFolder.CreateFileQueryWithOptions(_options);
                uint count = await queryResult.GetItemCountAsync();

                for (uint id = 0; id < count; id += 400)
                {
                    List<Task> list = new List<Task>();
                    for (uint i = id; i < id + 400; i += 100)
                        if (i < count)
                            list.Add(callback.Invoke(await queryResult.GetFilesAsync(i, 100)));
                        else
                            break;

                    Task.WaitAll(list.ToArray());
                    list.Clear();
                }
            }
        }
    }
}