using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys
{
    public static class FileReader
    {
        static FileReader()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static async Task<string> ReadText(StorageFile file, string secondaryEncoding)
        {
            string content = String.Empty;

            try
            {
                content = await FileIO.ReadTextAsync(file);
            }
            catch (Exception)
            {
                var encoding = Encoding.GetEncoding(secondaryEncoding);
                content = encoding.GetString((await FileIO.ReadBufferAsync(file)).ToArray());
            }

            return content;
        }

        public static async Task<List<string>> ReadLines(StorageFile file, string secondaryEncoding, int maxCount = 0, bool isReverse = false)
        {
            var text = await ReadText(file, secondaryEncoding);

            IEnumerable<string> split = text.Split(text.Contains("\n") ? '\n' : '\r');

            if (isReverse)
                split = split.Reverse();

            if (maxCount > 0)
                return split.Take(maxCount).Select(s => s.Trim()).ToList();

            return split.Select(s => s.Trim()).ToList();
        }
    }
}