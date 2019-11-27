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
            nameof(GroupName), typeof(string), typeof(ToggleButtonEx), new PropertyMetadata(String.Empty, GroupName_PropertyChangedCallback));

        public static readonly DependencyProperty IsSingleProperty = DependencyProperty.Register(
            nameof(IsSingle), typeof(bool), typeof(ToggleButtonEx), new PropertyMetadata(false));

        public ToggleButtonEx()
        {
            this.InitializeComponent();
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

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (!String.IsNullOrWhiteSpace(GroupName))
                SetUpGroup(this);
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
            if (!IsSingle || String.IsNullOrWhiteSpace(GroupName) || !String.IsNullOrWhiteSpace(GroupName) && AllButtons[GroupName].Any(b => b.IsChecked.GetValueOrDefault(false)))
                return;

            this.IsChecked = true;
        }

        private static void SetUpGroup(ToggleButtonEx button, string groupName = null)
        {
            string gn = groupName ?? button.GroupName;
            if (!AllButtons.ContainsKey(gn))
                lock (SetUpLocker)
                    if (!AllButtons.ContainsKey(gn))
                        AllButtons.Add(gn, new List<ToggleButtonEx>());

            AllButtons[gn].Add(button);
        }

        private static void GroupName_PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var theButton = (ToggleButtonEx) d;
            string oldName = e.OldValue.ToString();
            if (!String.IsNullOrWhiteSpace(oldName) && AllButtons.ContainsKey(oldName))
                AllButtons[oldName].Remove(theButton);

            SetUpGroup(theButton, e.NewValue.ToString());
        }
    }
}
