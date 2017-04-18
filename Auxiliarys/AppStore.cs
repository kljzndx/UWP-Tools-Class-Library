using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys
{
    public static class AppStore
    {
        public static async Task<bool> ShowItAppAsync(string developerName)
        {
            return await Launcher.LaunchUriAsync(new Uri($"ms-windows-store://publisher/?name={developerName}"));
        }

        public static async Task<bool> WriteReviewAsync(string appStoreID)
        {
            return await Launcher.LaunchUriAsync(new Uri($"ms-windows-store://review/?ProductId={appStoreID}"));
        }
    }
}
