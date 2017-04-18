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
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace HappyStudio.UwpToolsLibrary.Control
{
    [ContentProperty(Name = "ExpandContent")]
    public sealed partial class Expander : UserControl
    {

        public bool IsExPanded
        {
            get { return (bool)GetValue(IsExPandedProperty); }
            set { SetValue(IsExPandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsExPanded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExPandedProperty =
            DependencyProperty.Register("IsExPanded", typeof(bool), typeof(Expander), new PropertyMetadata(false));
        
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(Expander), new PropertyMetadata(String.Empty));
        
        public object ExpandContent
        {
            get { return (object)GetValue(ExpandContentProperty); }
            set { SetValue(ExpandContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExpandContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpandContentProperty =
            DependencyProperty.Register("ExpandContent", typeof(object), typeof(Expander), new PropertyMetadata(null));
        
        public Expander()
        {
            this.InitializeComponent();
        }

        private void Expand_Button_Click(object sender, RoutedEventArgs e)
        {
            IsExPanded = !IsExPanded;
        }
    }
}
