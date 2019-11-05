using LicitProd.Data.Repositories;
using LicitProd.UI.Uwp.Services;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LicitProd.UI.Uwp.Pages.TerminosYCondiciones
{
    public sealed partial class CrearTerminosYCondicionesPage : Page
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public CrearTerminosYCondicionesPage()
        {
            this.InitializeComponent();
        }

        private async void BtnAcept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Nombre)
                ||
                string.IsNullOrWhiteSpace(Descripcion))
            {
                MessageDialogService.Create("Debe completar todos los campos");
            }
            else
            {
                LoadingService.LoadingStart();
                (await (new TerminosYCondicionesRepository()).InsertDataAsync(new Entities.TerminosYCondiciones(Nombre, Descripcion)))
                    .Success(x =>
                    {
                        MessageDialogService.Create("Terminos y condiciones creado exitosamente", c =>
                        {
                            LoadingService.LoadingStop();
                            NavigationService.NavigatePop<Dashboard>();
                        }, null);

                    });
            }
        }
    }
}
