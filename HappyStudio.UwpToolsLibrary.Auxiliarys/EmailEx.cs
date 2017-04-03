using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys
{
    public static class EmailEx
    {
        public static async Task Send(string to, string subject, string body)
        {
            await Launcher.LaunchUriAsync(new Uri($"mailto:{to}?subject={subject}&body={body.Replace("\n", "%0A")}"));
        }
    }
}
