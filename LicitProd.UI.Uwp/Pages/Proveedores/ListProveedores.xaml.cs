using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;

namespace LicitProd.UI.Uwp.Pages.Proveedores
{
    public sealed partial class ListProveedores : Page
    {
        public ObservableCollection<Proveedor> Proveedores { get; set; } = new ObservableCollection<Proveedor>();
        public ListProveedores()
        {
            InitializeComponent();
            LoadingService.LoadingStart();
            LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            (await new ProveedoresRepository().Get())
           .Success(proveedores =>
           {
               proveedores?.ForEach(x=> Proveedores.Add(x));
               LoadingService.LoadingStop();
           })
           .Error(async x =>
           {
               MessageDialogService.Create("No hay Proveedores", c =>
               {
                   LoadingService.LoadingStop();
                   NavigationService.NavigatePop<Dashboard>();
               }, null);
           });
        }
        public void Group()
        {

        }

    }
}
