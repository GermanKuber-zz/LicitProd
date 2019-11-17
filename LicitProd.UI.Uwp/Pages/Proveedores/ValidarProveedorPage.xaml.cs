using Windows.UI.Xaml.Controls;

namespace LicitProd.UI.Uwp.Pages.Proveedores
{
    public sealed partial class ValidarProveedorPage : Page
    {
        public string Email { get; set; }
        public ValidarProveedorPage()
        {
            this.InitializeComponent();
        }

        private void BtnAccept_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
