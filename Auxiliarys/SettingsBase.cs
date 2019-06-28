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
        protected SettingsBase() : this(ApplicationData.Current.LocalSettings)
        {
        }

        protected SettingsBase(ApplicationDataContainer container)
        {
            SettingContainer = container;
            SetUpSettingFields();
        }

        protected SettingsBase(string containerKey) : this(ApplicationData.Current.LocalSettings.CreateContainer(containerKey, ApplicationDataCreateDisposition.Always))
        {
        }
        
        public readonly ApplicationDataContainer SettingContainer;

        public event PropertyChangedEventHandler PropertyChanged;

        [Obsolete("Automatic initialization has been achieved")]
        protected void InitializeSettingFields()
        {
        }

        private void SetUpSettingFields()
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
            if (SettingContainer.Values.ContainsKey(key) == false)
                SettingContainer.Values[key] = defaultValue;

            return (T) SettingContainer.Values[key];
        }

        public T GetSetting<T>(string key, string defaultValue, Func<string, T> converter)
        {
            if (SettingContainer.Values.ContainsKey(key) == false)
                SettingContainer.Values[key] = defaultValue;

            return converter.Invoke(SettingContainer.Values[key].ToString());
        }

        protected virtual void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field.Equals(value))
                return;

            field = value;
            OnPropertyChanged(propertyName);
        }

        protected virtual void SetSetting<T>(ref T field, T value, string settingName = null, string settingValue = null, [CallerMemberName] string propertyName = null)
        {
            if (field.Equals(value))
                return;

            if (settingValue is null)
            {
                if (typeof(T).GetTypeInfo().IsEnum)
                    SettingContainer.Values[settingName ?? propertyName] = value.ToString();
                else
                    SettingContainer.Values[settingName ?? propertyName] = value;
            }
            else
                SettingContainer.Values[settingName ?? propertyName] = settingValue;

            Set(ref field, value, propertyName);
        }

        public void RenameSettingKey(string oldKey, string newKey)
        {
            if (SettingContainer.Values.ContainsKey(oldKey))
            {
                SettingContainer.Values[newKey] = SettingContainer.Values[oldKey];
                SettingContainer.Values.Remove(oldKey);
            }
        }

        [Obsolete("Please use 'UpdateSettingValue' method")]
        public void RenameSettingValue<T>(string key, T oldValue, T newValue) => UpdateSettingValue(key, oldValue, newValue);

        public void UpdateSettingValue<T>(string key, T oldValue, T newValue)
        {
            if (SettingContainer.Values.ContainsKey(key) && SettingContainer.Values[key] == (object) oldValue)
                SettingContainer.Values[key] = newValue;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}