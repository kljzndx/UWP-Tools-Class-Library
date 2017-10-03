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
        private static readonly DependencyProperty CurrentBackgroundProperty = DependencyProperty.Register(
            nameof(CurrentBackground), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        private static readonly DependencyProperty CurrentForegroundProperty = DependencyProperty.Register(
            nameof(CurrentForeground), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

        private static readonly DependencyProperty CurrentBorderBrushProperty = DependencyProperty.Register(
            nameof(CurrentBorderBrush), typeof(Brush), typeof(ButtonEx), new PropertyMetadata(null));

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

        public static readonly DependencyProperty RingVisibilityProperty = DependencyProperty.Register(
            nameof(RingVisibility), typeof(Visibility), typeof(ButtonEx), new PropertyMetadata(Visibility.Collapsed));

        private ProgressRing _ring;

        public ButtonEx()
        {
            DefaultStyleKey = typeof(ButtonEx);

            IsEnabledChanged += ButtonEx_IsEnabledChanged;
            SizeChanged += ButtonEx_SizeChanged;
        }

        private double Size => Width <= Height ? Width : Height;

        public Brush CurrentBackground
        {
            get => (Brush) GetValue(CurrentBackgroundProperty);
            private set => SetValue(CurrentBackgroundProperty, value);
        }

        public Brush CurrentForeground
        {
            get => (Brush) GetValue(CurrentForegroundProperty);
            private set => SetValue(CurrentForegroundProperty, value);
        }

        public Brush CurrentBorderBrush
        {
            get => (Brush) GetValue(CurrentBorderBrushProperty);
            private set => SetValue(CurrentBorderBrushProperty, value);
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
            CurrentBackground = background;
            CurrentForeground = foreground;
            CurrentBorderBrush = borderBrush;
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            SwitchStyle(PointerOverBackground, PointerOverForeground, PointerOverBorderBrush);
            GoToState("PointerOver");
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            SwitchStyle(Background, Foreground, BorderBrush);
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
            SwitchStyle(Background, Foreground, BorderBrush);
            GoToState("PointerOver");
        }

        protected override void OnPointerCanceled(PointerRoutedEventArgs e)
        {
            SwitchStyle(Background, Foreground, BorderBrush);
            GoToState("PointerOver");
        }

        protected override void OnPointerCaptureLost(PointerRoutedEventArgs e)
        {
            SwitchStyle(Background, Foreground, BorderBrush);
            GoToState("PointerOver");
        }

        private void ButtonEx_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
                SwitchStyle(Background, Foreground, BorderBrush);
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
            SwitchStyle(Background, Foreground, BorderBrush);

            _ring = GetTemplateChild("ProgressRing") as ProgressRing;

            _ring.Width = Size;
            _ring.Height = Size;
        }
    }
}