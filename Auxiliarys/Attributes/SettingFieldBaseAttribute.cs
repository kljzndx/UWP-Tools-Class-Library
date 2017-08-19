using System;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class SettingFieldBaseAttribute : Attribute
    {
        protected SettingFieldBaseAttribute(string settingName, object defaultValue)
        {
            SettingName = settingName;
            DefaultValue = defaultValue;
        }

        public string SettingName { get; set; }
        public object DefaultValue { get; set; }
        public Func<string, object> Converter { get; set; }
    }
}