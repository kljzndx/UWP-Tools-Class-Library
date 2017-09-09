using System;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys.Attributes
{
    public sealed class SettingFieldByEnumAttribute : SettingFieldBaseAttribute
    {
        public SettingFieldByEnumAttribute(string settingName, Type enumType, string defaultValue) : base(settingName, defaultValue)
        {
            Converter = str => Enum.Parse(enumType, str);
        }
    }
}