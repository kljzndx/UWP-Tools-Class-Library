using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Composition;
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
        public FrostedGlassBackground()
        {
            this.InitializeComponent();
        }



        public Brush GlassBackgroundBrush
        {
            get { return (Brush)GetValue(GlassBackgroundBrushProperty); }
            set { SetValue(GlassBackgroundBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GlassBackgroundBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GlassBackgroundBrushProperty =
            DependencyProperty.Register("GlassBackgroundBrush", typeof(Brush), typeof(FrostedGlassBackground), new PropertyMetadata(null));



        public double GlassOpacity
        {
            get { return (double)GetValue(GlassOpacityProperty); }
            set { SetValue(GlassOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GlassOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GlassOpacityProperty =
            DependencyProperty.Register("GlassOpacity", typeof(double), typeof(FrostedGlassBackground), new PropertyMetadata(0.7));



        private void InitializeFrostedGlass(UIElement glassHost)
        {
            Visual hostVisual = ElementCompositionPreview.GetElementVisual(glassHost);
            Compositor compositor = hostVisual.Compositor;
            var backdropBrush = compositor.CreateHostBackdropBrush();
            var glassVisual = compositor.CreateSpriteVisual();
            glassVisual.Brush = backdropBrush;
            ElementCompositionPreview.SetElementChildVisual(glassHost, glassVisual);
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);
            glassVisual.StartAnimation("Size", bindSizeAnimation);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsMethodPresent(typeof(Compositor).FullName, "CreateHostBackdropBrush"))
            {
                this.FindName("Root_Grid");
                InitializeFrostedGlass(Glass_Rectangle);
            }
        }
    }
}
