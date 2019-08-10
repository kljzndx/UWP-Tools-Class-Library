using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace HappyStudio.UwpToolsLibrary.Control
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FrostedGlassBackground : UserControl
    {
        public static readonly DependencyProperty GlassBackgroundBrushProperty =
            DependencyProperty.Register(nameof(GlassBackgroundBrush), typeof(Brush), typeof(FrostedGlassBackground), new PropertyMetadata(null));

        public static readonly DependencyProperty GlassOpacityProperty =
            DependencyProperty.Register(nameof(GlassOpacity), typeof(double), typeof(FrostedGlassBackground), new PropertyMetadata(0.7));

        public FrostedGlassBackground()
        {
            this.InitializeComponent();
        }

        public Brush GlassBackgroundBrush
        {
            get => (Brush)GetValue(GlassBackgroundBrushProperty);
            set => SetValue(GlassBackgroundBrushProperty, value);
        }

        public double GlassOpacity
        {
            get => (double)GetValue(GlassOpacityProperty);
            set => SetValue(GlassOpacityProperty, value);
        }

        private void InitializeFrostedGlass()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.AcrylicBrush"))
            {
                AcrylicBrush brush = new AcrylicBrush();
                brush.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
                brush.TintColor = Colors.Transparent;
                brush.TintOpacity = 0;
                Glass_Rectangle.Fill = brush;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeFrostedGlass();
        }
    }
}
