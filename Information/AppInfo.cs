using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace HappyStudio.UwpToolsLibrary.Information
{
    public static class AppInfo
    {
        public static string Name { get => Package.Current.DisplayName; }
        public static string Version
        {
            get
            {
                var version = Package.Current.Id.Version;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }
    }
}
