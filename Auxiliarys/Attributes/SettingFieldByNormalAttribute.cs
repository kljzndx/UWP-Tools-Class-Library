using System;
using Windows.Foundation;
using Windows.Storage;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys.Attributes
{
    public sealed class SettingFieldByNormalAttribute : SettingFieldBaseAttribute
    {
        public SettingFieldByNormalAttribute(string settingName, byte defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, short defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, ushort defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, int defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, uint defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, long defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, ulong defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, float defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, double defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, bool defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, char defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, string defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, DateTime defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, TimeSpan defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, Guid defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, Point defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, Size defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, Rect defaultValue) : base(settingName, defaultValue)
        {
        }

        public SettingFieldByNormalAttribute(string settingName, ApplicationDataCompositeValue defaultValue) : base(settingName, defaultValue)
        {
        }
    }
}