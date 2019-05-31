using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace HappyStudio.UwpToolsLibrary.Control
{
    public sealed partial class FloatingActionButton : ContentControl, IInvokeProvider
    {
        public new static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
            nameof(BorderThickness), typeof(double), typeof(FloatingActionButton), new PropertyMetadata(0D));

        public FloatingActionButton()
        {
            this.InitializeComponent();
        }

        [Obsolete("Please use 'Content' property")]
        public string Glyph
        {
            get => (string) Content;
            set => Content = value;
        }

        public new double BorderThickness
        {
            get => (double) GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public event RoutedEventHandler Click;

        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
        
        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            e.Handled = true;
            if (IsEnabled)
                VisualStateManager.GoToState(this, "PointerOver", true);
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            e.Handled = true;
            if (IsEnabled)
                VisualStateManager.GoToState(this, "Normal", true);
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            e.Handled = true;
            if (IsEnabled)
                VisualStateManager.GoToState(this, "Pressed", true);
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            e.Handled = true;
            if (IsEnabled)
                VisualStateManager.GoToState(this, "Normal", true);
        }

        protected override void OnPointerCaptureLost(PointerRoutedEventArgs e)
        {
            e.Handled = true;
            if (IsEnabled)
                VisualStateManager.GoToState(this, "Normal", true);
        }

        protected override void OnPointerCanceled(PointerRoutedEventArgs e)
        {
            e.Handled = true;
            if (IsEnabled)
                VisualStateManager.GoToState(this, "Normal", true);
        }

        private void FloatingActionButton_OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool isEnabled = (bool) e.NewValue;
            if (isEnabled)
                VisualStateManager.GoToState(this, "Normal", true);
            else
                VisualStateManager.GoToState(this, "Disabled", true);
        }
        
        public void Invoke()
        {
            Click?.Invoke(this, new RoutedEventArgs());
        }
    }
}
