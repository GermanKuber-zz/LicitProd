using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Seguridad;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages.Compradores
{
    public sealed partial class NewCompradorPage : Page
    {
        public Comprador Comprador { get; set; } = new Comprador();
        public string Email { get; set; }
        public string Password { get; set; }

        public NewCompradorPage()
        {
            InitializeComponent();
        }

        private async void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            LoadingService.LoadingStart();

            var rol = (await new RolRepository().Get()).Result.FirstOrDefault(x => x.Nombre == "Comprador");
            Comprador.Usuario =  (new Usuario(Email, new HashService().Hash(Password), rol));

            (await new CompradoresServices().Registrar(Comprador))
                .Success(s =>
                {
                    MessageDialogService.Create("Comprador Registrado Existosamente", c =>
                    {
                        LoadingService.LoadingStop();
                        NavigationService.NavigatePop<Dashboard>();
                    }, null);

                })
                .Error(erros =>
                {
                    MessageDialogService.Create("Error al Crear el Comprador", c =>
                    {
                        LoadingService.LoadingStop();
                    }, null);
                });
        }
    }
}
