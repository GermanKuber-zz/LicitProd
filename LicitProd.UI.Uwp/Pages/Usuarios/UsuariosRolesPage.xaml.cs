using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Pages.Permisos;
using LicitProd.UI.Uwp.Services;
using TreeViewNode = Microsoft.UI.Xaml.Controls.TreeViewNode;

namespace LicitProd.UI.Uwp.Pages.Usuarios
{
    public sealed partial class UsuariosRolesPage : Page
    {
        public ObservableCollection<Permission> Permisos { get; set; } = new ObservableCollection<Permission>();

        public UsuariosRolesPage()
        {
            this.InitializeComponent();
            LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            (await new RolRepository().Get())
                .Success(roles =>
                {
                    roles?.ForEach(x => Permisos.Add(x));
                    var list = FillTree(roles, new List<TreeViewNode>());
                    list.ToList().ForEach(x => trvPermisos.RootNodes.Add(x));
                    LoadingService.LoadingStop();
                })
                .Error(x =>
                {
                    MessageDialogService.Create("No hay Roles", c =>
                    {
                        LoadingService.LoadingStop();
                        NavigationService.NavigatePop<Dashboard>();
                    }, null);
                });
        }
        private IList<TreeViewNode> FillTree(IEnumerable<Permission> permisos, IList<TreeViewNode> nodes)
        {
            permisos?.ToList().ForEach(permiso => nodes.Add(new TreeViewNodeCustom(permiso)));
            return nodes;
        }

        private void BtnAccept_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
