using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace HappyStudio.UwpToolsLibrary.Control
{
    public sealed class ButtonEx : ButtonBase
    {

        public static readonly DependencyProperty NormalBackgroundProperty = DependencyProperty.Register(
            nameof(NormalBackground), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty NormalForegroundProperty = DependencyProperty.Register(
            nameof(NormalForeground), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty NormalBorderBrushProperty = DependencyProperty.Register(
            nameof(NormalBorderBrush), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty PointerOverBackgroundProperty = DependencyProperty.Register(
            nameof(PointerOverBackground), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty PointerOverForegroundProperty = DependencyProperty.Register(
            nameof(PointerOverForeground), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty PointerOverBorderBrushProperty = DependencyProperty.Register(
            nameof(PointerOverBorderBrush), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register(
            nameof(PressedBackground), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register(
            nameof(PressedForeground), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty PressedBorderBrushProperty = DependencyProperty.Register(
            nameof(PressedBorderBrush), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty DisabledBackgroundProperty = DependencyProperty.Register(
            nameof(DisabledBackground), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty DisabledForegroundProperty = DependencyProperty.Register(
            nameof(DisabledForeground), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        public static readonly DependencyProperty DisabledBorderBrushProperty = DependencyProperty.Register(
            nameof(DisabledBorderBrush), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        private ProgressRing _ring;
        private double Size => Width <= Height ? Width : Height;
        
        public ButtonEx()
        {
            DefaultStyleKey = typeof(ButtonEx);
            
            IsEnabledChanged += ButtonEx_IsEnabledChanged;
            SizeChanged += ButtonEx_SizeChanged;
        }

        public Brush NormalBackground
        {
            get => (Brush) GetValue(NormalBackgroundProperty);
            set => SetValue(NormalBackgroundProperty, value);
        }

        public Brush NormalForeground
        {
            get => (Brush) GetValue(NormalForegroundProperty);
            set => SetValue(NormalForegroundProperty, value);
        }

        public Brush NormalBorderBrush
        {
            get => (Brush) GetValue(NormalBorderBrushProperty);
            set => SetValue(NormalBorderBrushProperty, value);
        }

        public Brush PointerOverBackground
        {
            get => (Brush) GetValue(PointerOverBackgroundProperty);
            set => SetValue(PointerOverBackgroundProperty, value);
        }

        public Brush PointerOverForeground
        {
            get => (Brush) GetValue(PointerOverForegroundProperty);
            set => SetValue(PointerOverForegroundProperty, value);
        }

        public Brush PointerOverBorderBrush
        {
            get => (Brush) GetValue(PointerOverBorderBrushProperty);
            set => SetValue(PointerOverBorderBrushProperty, value);
        }

        public Brush PressedBackground
        {
            get => (Brush) GetValue(PressedBackgroundProperty);
            set => SetValue(PressedBackgroundProperty, value);
        }

        public Brush PressedForeground
        {
            get => (Brush) GetValue(PressedForegroundProperty);
            set => SetValue(PressedForegroundProperty, value);
        }

        public Brush PressedBorderBrush
        {
            get => (Brush) GetValue(PressedBorderBrushProperty);
            set => SetValue(PressedBorderBrushProperty, value);
        }

        public Brush DisabledBackground
        {
            get => (Brush) GetValue(DisabledBackgroundProperty);
            set => SetValue(DisabledBackgroundProperty, value);
        }

        public Brush DisabledForeground
        {
            get => (Brush) GetValue(DisabledForegroundProperty);
            set => SetValue(DisabledForegroundProperty, value);
        }

        public Brush DisabledBorderBrush
        {
            get => (Brush) GetValue(DisabledBorderBrushProperty);
            set => SetValue(DisabledBorderBrushProperty, value);
        }

        public static readonly DependencyProperty RingVisibilityProperty = DependencyProperty.Register(
            nameof(RingVisibility), typeof(Visibility), typeof(ButtonEx), new PropertyMetadata(Visibility.Collapsed));

        public Visibility RingVisibility
        {
            get => (Visibility) GetValue(RingVisibilityProperty);
            set
            {
                SetValue(RingVisibilityProperty, value);

                IsEnabled = value == Visibility.Collapsed;
            }
        }
        
        private bool GoToState(string stateName)
        {
            return VisualStateManager.GoToState(this, stateName, true);
        }

        private void SwitchStyle(Brush background, Brush foreground, Brush borderBrush)
        {
            Background = background;
            Foreground = foreground;
            BorderBrush = borderBrush;
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            SwitchStyle(PointerOverBackground, PointerOverForeground, PointerOverBorderBrush);
            GoToState("PointerOver");
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            SwitchStyle(NormalBackground, NormalForeground, NormalBorderBrush);
            GoToState("PointerOver");
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
                return;

            SwitchStyle(PressedBackground, PressedForeground, PressedBorderBrush);
            GoToState("Pressed");
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            SwitchStyle(NormalBackground, NormalForeground, NormalBorderBrush);
            GoToState("PointerOver");
        }

        protected override void OnPointerCanceled(PointerRoutedEventArgs e)
        {
            SwitchStyle(NormalBackground, NormalForeground, NormalBorderBrush);
            GoToState("PointerOver");
        }

        protected override void OnPointerCaptureLost(PointerRoutedEventArgs e)
        {
            SwitchStyle(NormalBackground, NormalForeground, NormalBorderBrush);
            GoToState("PointerOver");
        }

        private void ButtonEx_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
                SwitchStyle(NormalBackground, NormalForeground, NormalBorderBrush);
            else
                SwitchStyle(DisabledBackground, DisabledForeground, DisabledBorderBrush);

            GoToState("PointerOver");
        }

        private void ButtonEx_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _ring.Width = Size;
            _ring.Height = Size;
        }

        protected override void OnApplyTemplate()
        {
            SwitchStyle(NormalBackground, NormalForeground, NormalBorderBrush);

            _ring = GetTemplateChild("ProgressRing") as ProgressRing;

            _ring.Width = Size;
            _ring.Height = Size;
        }
    }
}