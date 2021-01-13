using HappyStudio.UwpToolsLibrary.Auxiliarys;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<string> _strs = new ObservableCollection<string>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void ScanFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            _strs.Clear();
            var fc = new LibraryFolderScanner(await StorageLibrary.GetLibraryAsync(KnownLibraryId.Music));
            await fc.ScanByFolderQuery(async fs =>
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (var item in fs)
                    {
                        _strs.Add(item.Path);
                    }
                })
            );
        }
    }
}
