using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Services;
using LicitProd.UI.Uwp.Pages.Usuarios;
using LicitProd.UI.Uwp.Services;

namespace LicitProd.UI.Uwp.Pages.Permisos
{
    public sealed partial class AdministrarPermisosPage : Page
    {
        private PageUtilities _pageUtilities = new PageUtilities();
        public ObservableCollection<Rol> Roles { get; set; } = new ObservableCollection<Rol>();
        public Rol RolSolected { get; set; }
        public AdministrarPermisosPage()
        {
            this.InitializeComponent();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            (await new RolRepository().GetAllAsync())
                .Success(x => x.ForEach(s => Roles.Add(s)));

        }
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var response = await new RolesServices().EliminarAsync(RolSolected);
            if (response.SuccessResult)
                _pageUtilities.ShowMessageDialog("Rol borrado exitosamente", () => NavigationService.Navigate<DashboardPage>());
            else
                _pageUtilities.ShowMessageDialog(response.Errors.First(), () => { });

        }

        private void MenuFlyoutItem_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate<CrearPermisosPage>();
        }

        //    var rolesToDelete = new List<Permission>();
        //    trvPermisos.SelectedNodes.ToList().Where(x =>
        //    {
        //        var node = (TreeViewNodeCustom)x;
        //        if (node.Data is Rol)
        //        {
        //            if (!ExistPermission(rolesToDelete, node.Data))
        //                rolesToDelete.Add(node.Data);
        //        }
        //        return false;
        //    }).ToList();
        //    var someDisableToDelete = rolesToDelete.Where(x => !x.ByDefault && (x is Rol)).ToList();
        //    await new RolesServices().EliminarAsync(someDisableToDelete);
        //    MessageDialogService.Create("Permisos borrados exitosamente", async c =>
        //    {
        //        LoadingService.LoadingStop();
        //        await LoadDataAsync();
        //    }, null);
    }
}
