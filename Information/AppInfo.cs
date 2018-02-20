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
        public static string Name { get; }
        public static string Version { get; }
        public static string Path { get; }

        static AppInfo()
        {
            Package current = Package.Current;
            Name = current.DisplayName;

            var version = current.Id.Version;
            Version = $"{version.Major}.{version.Minor}.{version.Build}";

            Path = current.InstalledLocation.Path;
        }
    }
}
