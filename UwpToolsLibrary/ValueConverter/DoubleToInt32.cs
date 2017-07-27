using System;
using Windows.UI.Xaml.Data;

namespace HappyStudio.UwpToolsLibrary.ValueConverter
{
    public class DoubleToInt32:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (int)Math.Ceiling((double) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (double) ((int) value);
        }
    }
}