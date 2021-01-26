using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace HappyStudio.UwpToolsLibrary.Control
{
    public sealed class ReelDialog : ContentControl
    {
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(object), typeof(ReelDialog), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register(nameof(HeaderTemplate), typeof(DataTemplate), typeof(ReelDialog), new PropertyMetadata(null));

        public ReelDialog()
        {
            this.DefaultStyleKey = typeof(ReelDialog);
        }

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate) GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }

        public void Show()
        {
            VisualStateManager.GoToState(this, "Show_VisualState", false);
        }

        public void Show(string content)
        {
            Show();
            this.Content = content;
        }

        public void Show(string header, string content)
        {
            Show(content);
            this.Header = header;
        }

        public void Hide()
        {
            VisualStateManager.GoToState(this, "Hide_VisualState", false);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var cb = (Button)this.GetTemplateChild("Close_Button");
            cb.Click -= Close_Button_Click;
            cb.Click += Close_Button_Click;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
