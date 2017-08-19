using System;
using Windows.UI.Xaml.Data;

namespace HappyStudio.UwpToolsLibrary.ValueConverter
{
    public class BoolToBoolNullable : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool?) ((bool) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool) ((bool?) value);
        }
    }
}