using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace HappyStudio.UwpToolsLibrary.Control
{
    public sealed partial class ToggleButtonEx : ToggleButton
    {
        private static readonly object SetUpLocker = new object();
        private static readonly Dictionary<string, List<ToggleButtonEx>> AllButtons = new Dictionary<string, List<ToggleButtonEx>>();

        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.Register(
            nameof(GroupName), typeof(string), typeof(ToggleButtonEx), new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty IsSingleProperty = DependencyProperty.Register(
            nameof(IsSingle), typeof(bool), typeof(ToggleButtonEx), new PropertyMetadata(false));

        public ToggleButtonEx()
        {
            this.InitializeComponent();
            if (!String.IsNullOrWhiteSpace(GroupName))
                SetUpGroup(this);
        }

        public string GroupName
        {
            get => (string) GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }

        public bool IsSingle
        {
            get => (bool) GetValue(IsSingleProperty);
            set => SetValue(IsSingleProperty, value);
        }
        
        private void ToggleButtonEx_Checked(object sender, RoutedEventArgs e)
        {
            if (IsSingle && !String.IsNullOrWhiteSpace(GroupName))
                foreach (var buttonEx in AllButtons[GroupName])
                    if (buttonEx != this && buttonEx.IsSingle)
                        buttonEx.IsChecked = false;
        }

        private void ToggleButtonEx_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (!IsSingle || !String.IsNullOrWhiteSpace(GroupName) && AllButtons[GroupName].Any(b => b.IsChecked.GetValueOrDefault(false)))
                return;

            this.IsChecked = true;
        }

        private static void SetUpGroup(ToggleButtonEx button)
        {
            if (!AllButtons.ContainsKey(button.GroupName))
                lock (SetUpLocker)
                    if (!AllButtons.ContainsKey(button.GroupName))
                        AllButtons.Add(button.GroupName, new List<ToggleButtonEx>{button});

            AllButtons[button.GroupName].Add(button);
        }
    }
}
