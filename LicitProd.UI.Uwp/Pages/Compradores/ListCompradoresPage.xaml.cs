using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;

namespace LicitProd.UI.Uwp.Pages.Compradores
{
    public sealed partial class ListCompradoresPage : Page
    {
        public ObservableCollection<Comprador> Compradores { get; set; } = new ObservableCollection<Comprador>();
        public ListCompradoresPage()
        {
            InitializeComponent();
            LoadingService.LoadingStart();
            LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            (await new CompradorRepository().GetAsync())
           .Success(compradores =>
           {
               compradores?.ForEach(x=> Compradores.Add(x));
               LoadingService.LoadingStop();
           })
           .Error(async x =>
           {
               MessageDialogService.Create("No hay Compradores", c =>
               {
                   LoadingService.LoadingStop();
                   NavigationService.NavigatePop<Dashboard>();
               }, null);
           });
        }
        public void Group()
        {

        }

        private void ButtonBase_OnClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }


        private void ButtonEliminar_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void MenuFlyoutItem_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate<NewCompradorPage>();
        }
    }
}
