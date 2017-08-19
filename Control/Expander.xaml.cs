using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace HappyStudio.UwpToolsLibrary.Control
{
    [ContentProperty(Name = "ExpandContent")]
    public sealed partial class Expander : UserControl
    {
        // Using a DependencyProperty as the backing store for IsExPanded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExPandedProperty =
            DependencyProperty.Register(nameof(IsExPanded), typeof(bool), typeof(Expander), new PropertyMetadata(false));

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(object), typeof(Expander), new PropertyMetadata("Header"));

        // Using a DependencyProperty as the backing store for ExpandContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpandContentProperty =
            DependencyProperty.Register(nameof(ExpandContent), typeof(object), typeof(Expander), new PropertyMetadata(null));

        public static readonly DependencyProperty NormalStateSignProperty;

        public static readonly DependencyProperty ActvateStateSignProperty;

        public static readonly DependencyProperty IsDisplaySignProperty = DependencyProperty.Register(
            nameof(IsDisplaySign), typeof(bool), typeof(Expander), new PropertyMetadata(true));

        static Expander()
        {
            var family = new FontFamily("Segoe MDL2 Assets");
            double fontSize = 24;

            NormalStateSignProperty = DependencyProperty.Register(nameof(NormalStateSign), typeof(object), typeof(Expander), new PropertyMetadata(new TextBlock {FontFamily = family, FontSize = fontSize, Text = "\uE00F"}));
            ActvateStateSignProperty = DependencyProperty.Register(nameof(ActvateStateSign), typeof(object), typeof(Expander), new PropertyMetadata(new TextBlock {FontFamily = family, FontSize = fontSize, Text = "\uE011"}));
        }

        public Expander()
        {
            InitializeComponent();
        }

        public bool IsExPanded
        {
            get => (bool) GetValue(IsExPandedProperty);
            set => SetValue(IsExPandedProperty, value);
        }

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public object ExpandContent
        {
            get => GetValue(ExpandContentProperty);
            set => SetValue(ExpandContentProperty, value);
        }

        public object NormalStateSign
        {
            get => GetValue(NormalStateSignProperty);
            set => SetValue(NormalStateSignProperty, value);
        }

        public object ActvateStateSign
        {
            get => GetValue(ActvateStateSignProperty);
            set => SetValue(ActvateStateSignProperty, value);
        }

        public bool IsDisplaySign
        {
            get => (bool) GetValue(IsDisplaySignProperty);
            set => SetValue(IsDisplaySignProperty, value);
        }
        
    }
}