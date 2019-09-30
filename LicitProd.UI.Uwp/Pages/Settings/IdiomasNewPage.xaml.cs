using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;


namespace LicitProd.UI.Uwp.Pages.Settings
{
    public sealed partial class IdiomasNewPage : Page
    {
        public ObservableCollection<TraduccionValue> Traducciones { get; set; } =
            new ObservableCollection<TraduccionValue>();

        public string NewIdiomaName { get; set; }
        public IdiomasNewPage()
        {
            this.InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        {
            LoadingService.LoadingStart();

            (await (new TraduccionesRepository()
                    .GetAllKeys())).Success(x =>
                {
                    x.ForEach(s => Traducciones.Add(s));
                    LoadingService.LoadingStop();
                }).Error(errors => ShowMessageDialog(errors.First()));

        }

        private void ShowMessageDialog(string message, Action callBackSuccess = null)
        {
            MessageDialogService.Create(message, c =>
            {
                callBackSuccess?.Invoke();
                LoadingService.LoadingStop();
            }, null);

        }
        private void ApbCancel_OnClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadingService.LoadingStart();

            MessageDialogService.Create("¿Esta seguro que desea descargar los cambios?", c =>
            {
                LoadingService.LoadingStop();
                NavigationService.NavigatePop<IdiomasSettingsPage>();
            }, command => { });
        }

        private async void ApbAccept_OnClick(object sender, RoutedEventArgs e)
        {
            LoadingService.LoadingStart();

            var newIdioma = new Idioma(NewIdiomaName, Traducciones.ToList());
            (await new IdiomaServices().Crear(newIdioma))
                .Success(x => { ShowMessageDialog($"El idioma {newIdioma.Nombre} fue creado existosamente"); })
                .Error(errors => { ShowMessageDialog(errors.First()); });
        }
    }
}
