using System;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys.Attributes
{
    public sealed class SettingFieldByEnumAttribute : SettingFieldBaseAttribute
    {
        public SettingFieldByEnumAttribute(string settingName, Enum defaultValue) : base(settingName, defaultValue)
        {
            Converter = str => Enum.Parse(defaultValue.GetType(), str);
        }
    }
}