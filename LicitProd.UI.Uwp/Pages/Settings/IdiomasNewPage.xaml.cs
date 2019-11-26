using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Services;
using LicitProd.UI.Uwp.Pages.Usuarios;
using LicitProd.UI.Uwp.Services;


namespace LicitProd.UI.Uwp.Pages.Settings
{
    public sealed partial class IdiomasNewPage : Page
    {
        private PageUtilities _pageUtilities = new  PageUtilities();
        public ObservableCollection<TraduccionValue> Traducciones { get; set; } =
            new ObservableCollection<TraduccionValue>();

        public string NewIdiomaName { get; set; }
        public IdiomasNewPage()
        {
            InitializeComponent();
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
                }).Error(errors => _pageUtilities.ShowMessageDialog(errors.First()));

        }

       
        private void ApbCancel_OnClick(object sender, RoutedEventArgs e)
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
                .Success(x => { _pageUtilities.ShowMessageDialog($"El idioma {newIdioma.Nombre} fue creado existosamente"); })
                .Error(errors => { _pageUtilities.ShowMessageDialog(errors.First()); });
        }
    }
}
