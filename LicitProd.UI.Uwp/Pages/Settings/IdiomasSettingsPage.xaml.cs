using System.Collections.Generic;
using LicitProd.Data.Repositories;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Entities;
using LicitProd.Mappers;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages.Settings
{
    public sealed partial class IdiomasSettingsPage : Page
    {
        private IdiomaViewModel _idiomaSelected = new IdiomaViewModel();

        public ObservableCollection<IdiomaViewModel> Idiomas { get; set; } = new ObservableCollection<IdiomaViewModel>();

        public ObservableCollection<TraduccionValue> Traducciones { get; set; } =
            new ObservableCollection<TraduccionValue>();
        public IdiomaViewModel IdiomaSelected
        {
            get
            {
                return _idiomaSelected;
            }
            set
            {
                _idiomaSelected.Idioma?.SetTraducciones(Traducciones.ToList());
                _idiomaSelected = value;
                Traducciones.Clear();
                _idiomaSelected.Traducciones.ToList().ForEach(x => Traducciones.Add(x));
            }
        }

        public IdiomasSettingsPage()
        {
            InitializeComponent();
            LoadingService.LoadingStart();

            LoadData();
        }

        private async void LoadData()
        {
            (await (new IdiomasRepository()
                .Get())).Success(x =>
                {
                    x.ForEach(s => Idiomas.Add(new IdiomaViewModel(s)));
                    LoadingService.LoadingStop();
                })
                .Error(erros =>
                {
                    MessageDialogService.Create(erros.First(), c =>
                    {
                        LoadingService.LoadingStop();
                        NavigationService.Close();
                    }, null);

                });

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Idioma_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void ApbCancel_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ApbAccept_OnClick(object sender, RoutedEventArgs e)
        {
            LoadingService.LoadingStart();

            MessageDialogService.Create("¿Desea salvar todos los cambios realizados en los idiomas?", async command =>
                {
                    foreach (var idioma in Idiomas)
                    {
                        await new IdiomasRepository()
                            .UpdateDataAsync(idioma.Idioma);
                    }
                    AsyncHelper.CallAsyncMethod(() => new IdiomasRepository().GetByName(SettingsServices.Idioma.Nombre))
                        .Success(idioma =>
                            SettingsServices.SetIdioma(idioma));
                    LoadingService.LoadingStop();
                },
                command =>
                {
                    LoadingService.LoadingStop();
                });
        }

        private void MenuFlyout_Closed(object sender, object e)
        {

        }

        private void MenuFlyout_Opened(object sender, object e)
        {

        }

        private void MenuFlyoutItem_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate<IdiomasNewPage>();
        }
    }
}
