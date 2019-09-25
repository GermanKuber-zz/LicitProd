using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using Microsoft.UI.Xaml.Controls;
using TreeViewNode = Microsoft.UI.Xaml.Controls.TreeViewNode;
using LicitProd.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages.Permisos
{
    public class TreeViewNodeCustom : TreeViewNode
    {
        public Permission Data { get; set; }

        public TreeViewNodeCustom(Permission permiso) : base()
        {
            Content = permiso.Nombre.ToString();
            Data = permiso;
        }

        public TreeViewNodeCustom()
        {

        }
    }


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPermisos : Page
    {
        public ObservableCollection<Permission> Permisos { get; set; } = new ObservableCollection<Permission>();

        public AdminPermisos()
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
            permisos?.ToList().ForEach(permiso =>
            {
                var mainNode = new TreeViewNodeCustom(permiso);
                if (permiso.Permissions.Any())
                {
                    FillTree(permiso.Permissions, new List<TreeViewNode>())?.ToList()
                        .ForEach(p => mainNode.Children.Add(p));
                }
                nodes.Add(mainNode);
            });
            return nodes;
        }

        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var roles = trvPermisos.SelectedNodes.ToList().Where(x =>
            {
                var node = (TreeViewNodeCustom)x;
                if (node.Data is Rol)
                    return true;
                return false;
            }).ToList();
            var permisos = trvPermisos.SelectedNodes.ToList().Where(x =>
            {
                var nodeParse = (TreeViewNodeCustom)x;
                var inParent = roles.Any(s => s == nodeParse.Parent);
                if (nodeParse.Data is SinglePermission && !inParent)
                    return true;
                return false;
            }).ToList();
            var newRol = new Rol(txtNewPermiso.Text);
            newRol.Permissions.AddRange(roles?.Select(node => ((TreeViewNodeCustom)node).Data));
            newRol.Permissions.AddRange(permisos?.Select(node => ((TreeViewNodeCustom)node).Data));
            (await new RolesServices().CreatAsync(newRol))
                                 .Success(async x => await LoadDataAsync()); ;
        }


    }
}
