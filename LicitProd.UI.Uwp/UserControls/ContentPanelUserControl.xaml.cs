using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LicitProd.UI.Uwp.UserControls
{
    public sealed partial class ContentPanelUserControl : UserControl
    {
        public static readonly DependencyProperty MainContentProperty =
            DependencyProperty.Register(
                "MainContent",
                typeof(object),
                typeof(ContentPanelUserControl),
                new PropertyMetadata(default(object)));

        public object MainContent
        {
            get { return (object)GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(ContentPanelUserControl), new PropertyMetadata(null));
        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public ContentPanelUserControl()
        {
            InitializeComponent();
        }
    }
}
