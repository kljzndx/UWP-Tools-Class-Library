using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Windows.Storage;
using HappyStudio.UwpToolsLibrary.Auxiliarys.Attributes;

namespace HappyStudio.UwpToolsLibrary.Auxiliarys
{
    public abstract class SettingsBase : INotifyPropertyChanged
    {
        public ApplicationDataContainer SettingObject => ApplicationData.Current.LocalSettings;

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitializeSettingFields()
        {
            var fieldInfos = this.GetType().GetTypeInfo().DeclaredFields;

            foreach (FieldInfo field in fieldInfos)
            {
                SettingFieldBaseAttribute settingField = field.GetCustomAttribute<SettingFieldBaseAttribute>();

                if(settingField is null)
                    continue;

                if (settingField.Converter != null)
                    field.SetValue(this, GetSetting(settingField.SettingName, settingField.DefaultValue.ToString(), settingField.Converter));
                else
                    field.SetValue(this, GetSetting(settingField.SettingName, settingField.DefaultValue));
            }
        }

        public T GetSetting<T>(string key, T defaultValue)
        {
            if (SettingObject.Values.ContainsKey(key) == false)
                SettingObject.Values[key] = defaultValue;

            return (T) SettingObject.Values[key];
        }

        public T GetSetting<T>(string key, string defaultValue, Func<string, T> converter)
        {
            if (SettingObject.Values.ContainsKey(key) == false)
                SettingObject.Values[key] = defaultValue;

            return converter.Invoke(SettingObject.Values[key].ToString());
        }

        protected virtual void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field.Equals(value))
                return;

            field = value;
            OnPropertyChanged(propertyName);
        }

        protected virtual void SetSetting<T>(ref T field, T value, string settingName = null, [CallerMemberName] string propertyName = null)
        {
            if (field.Equals(value))
                return;

            SettingObject.Values[settingName ?? propertyName] = value;
            Set(ref field, value, propertyName);
        }

        public void RenameSettingKey(string oldKey, string newKey)
        {
            if (SettingObject.Values.ContainsKey(oldKey))
            {
                SettingObject.Values[newKey] = SettingObject.Values[oldKey];
                SettingObject.Values.Remove(oldKey);
            }
        }

        public void RenameSettingValue<T>(string key, T oldValue, T newValue)
        {
            if (SettingObject.Values.ContainsKey(key) && SettingObject.Values[key] == (object) oldValue)
                SettingObject.Values[key] = newValue;
        }
    }
}