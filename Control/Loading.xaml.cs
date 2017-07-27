using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HappyStudio.UwpToolsLibrary.Control.Enums;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace HappyStudio.UwpToolsLibrary.Control
{
    public sealed partial class Loading : UserControl
    {
        public static readonly DependencyProperty LoadingWidthProperty = DependencyProperty.Register(
            nameof(LoadingWidth), typeof(double), typeof(Loading), new PropertyMetadata(50D));

        public static readonly DependencyProperty LoadingHeightProperty = DependencyProperty.Register(
            nameof(LoadingHeight), typeof(double), typeof(Loading), new PropertyMetadata(50D));

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            nameof(Minimum), typeof(double), typeof(Loading), new PropertyMetadata(0D));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            nameof(Maximum), typeof(double), typeof(Loading), new PropertyMetadata(100D));

        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
            nameof(Position), typeof(double), typeof(Loading), new PropertyMetadata(0D));

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            nameof(Message), typeof(string), typeof(Loading), new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty TextAndProgressMarginProperty = DependencyProperty.Register(
            nameof(TextAndProgressMargin), typeof(Thickness), typeof(Loading), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty LoadingStyleTypeProperty = DependencyProperty.Register(
            nameof(LoadingStyleType), typeof(LoadingStyleType), typeof(Loading), new PropertyMetadata(LoadingStyleType.Ring));

        public Loading()
        {
            InitializeComponent();
        }

        public double LoadingWidth
        {
            get => (double) GetValue(LoadingWidthProperty);
            set => SetValue(LoadingWidthProperty, value);
        }

        public double LoadingHeight
        {
            get => (double) GetValue(LoadingHeightProperty);
            set => SetValue(LoadingHeightProperty, value);
        }

        public double Minimum
        {
            get => (double) GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public double Maximum
        {
            get => (double) GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public double Position
        {
            get => (double) GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        public string Message
        {
            get => (string) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public Thickness TextAndProgressMargin
        {
            get => (Thickness) GetValue(TextAndProgressMarginProperty);
            set => SetValue(TextAndProgressMarginProperty, value);
        }

        public LoadingStyleType LoadingStyleType
        {
            get => (LoadingStyleType) GetValue(LoadingStyleTypeProperty);
            set => SetValue(LoadingStyleTypeProperty, value);
        }


        public void Clear()
        {
            Ring.IsActive = false;
            Ring.Visibility = Visibility.Collapsed;
            _progressBar.Visibility = Visibility.Collapsed;
        }

        public void Show()
        {
            Root_Border.Visibility = Visibility.Visible;

            if (LoadingStyleType == LoadingStyleType.Ring)
            {
                Ring.Visibility = Visibility.Visible;
                Ring.IsActive = true;
            }
            else
            {
                _progressBar.Visibility = Visibility.Visible;
                _progressBar.Value = Minimum;
            }

            Show_Storyboard.Begin();
        }

        public void Hide()
        {
            if (!Position.Equals(Maximum))
                Position = Maximum;

            Hide_Storyboard.Begin();
        }

        private void Hide_Storyboard_OnCompleted(object sender, object e)
        {
            Clear();
            Root_Border.Visibility = Visibility.Collapsed;
        }
    }
}