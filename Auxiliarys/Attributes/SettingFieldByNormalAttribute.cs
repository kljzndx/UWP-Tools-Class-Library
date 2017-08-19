using System;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys.Attributes
{
    public sealed class SettingFieldByNormalAttribute : SettingFieldBaseAttribute
    {
        public SettingFieldByNormalAttribute(string settingName, object defaultValue) : base(settingName, defaultValue)
        {
        }
    }
}