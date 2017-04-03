using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys
{
    public static class MessageBox
    {
        public static async Task<bool> ShowAsync(string content, string closeButtonContent)
        {
            MessageDialog box = new MessageDialog(content);
            box.Commands.Add(new UICommand(closeButtonContent));
            await box.ShowAsync();
            return true;
        }

        public static async Task<bool> ShowAsync(string title, string content, string closeButtonContent)
        {
            MessageDialog box = new MessageDialog(content, title);
            box.Commands.Add(new UICommand(closeButtonContent));
            await box.ShowAsync();
            return true;
        }

        public static async Task<bool> ShowAsync(string title, string content, Dictionary<string, UICommandInvokedHandler> buttons)
        {
            MessageDialog box = new MessageDialog(content, title);
            foreach (var item in buttons)
                box.Commands.Add(new UICommand(item.Key, item.Value));
            await box.ShowAsync();
            return true;
        }

        public static async Task<bool> ShowAsync(string title, string content, Dictionary<string, UICommandInvokedHandler> buttons, string closeButtonContent)
        {
            MessageDialog box = new MessageDialog(content, title);
            foreach (var item in buttons)
                box.Commands.Add(new UICommand(item.Key, item.Value));
            box.Commands.Add(new UICommand(closeButtonContent));
            await box.ShowAsync();
            return true;
        }
    }
}
