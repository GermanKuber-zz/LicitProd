using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Entities;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages.Proveedores
{

    public sealed partial class NewProveedor : Page
    {
        public Proveedor Proveedor { get; set; } = new Proveedor();
        public string Email { get; set; }
        public string Password { get; set; }

        public NewProveedor()
        {
            InitializeComponent();
        }

        private async void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            LoadingService.LoadingStart();



            (await new ProveedoresServices().Registrar(Proveedor, Email, Password))
                .Success(s =>
                {
                    MessageDialogService.Create("Proveedor Registrador Existosamente", c =>
                    {
                        LoadingService.LoadingStop();
                        NavigationService.NavigatePop<Dashboard>();
                    }, null);

                })
                .Error(erros =>
                {
                    MessageDialogService.Create("Error al Crear el Proveedor", c =>
                    {
                        LoadingService.LoadingStop();
                    }, null);
                });
        }
    }
}
