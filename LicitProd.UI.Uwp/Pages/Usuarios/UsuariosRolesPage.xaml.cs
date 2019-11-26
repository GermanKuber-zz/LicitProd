using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Seguridad;
using LicitProd.Services;
using LicitProd.UI.Uwp.Pages.Permisos;
using LicitProd.UI.Uwp.Services;
using TreeViewNode = Microsoft.UI.Xaml.Controls.TreeViewNode;

namespace LicitProd.UI.Uwp.Pages.Usuarios
{
    public sealed partial class UsuariosRolesPage : Page
    {
        public bool EditModeEnable { get; set; } = false;

        private PageUtilities _pageUtilities = new PageUtilities();
        private UsuarioRepository _usuarioRepository;
        public ObservableCollection<Usuario> Usuarios { get; set; } = new ObservableCollection<Usuario>();
        public Usuario SelectedUsuario { get; set; }
        public ObservableCollection<Rol> Permisos { get; set; } = new ObservableCollection<Rol>();
        public Rol PermisoSelected { get; set; }

        public UsuariosRolesPage()
        {
            InitializeComponent();
            LoadDataAsync();
            _usuarioRepository = new UsuarioRepository();
        }
        private async Task LoadDataAsync()
        {
            LoadingService.LoadingStart();

            (await new RolRepository().Get())
                .Success(roles =>
                {
                    roles?.ForEach(x => Permisos.Add(x));
                })
                .Error(x =>
                {
                    _pageUtilities.ShowMessageDialog("No hay Roles");
                });


            (await _usuarioRepository.Get())
                .Success(usuarios =>
                {
                    usuarios?.Where(s=> s.Id != IdentityServices.Instance.GetUserLogged().Id).ToList().ForEach(x => Usuarios.Add(x));
                    SelectedUsuario = usuarios.First();
                })
                .Error(x => { _pageUtilities.ShowMessageDialog("No hay Roles"); });
            LoadingService.LoadingStop();

        }
        private IList<TreeViewNode> FillTree(IEnumerable<Permission> permisos, IList<TreeViewNode> nodes)
        {
            permisos?.ToList().ForEach(permiso => nodes.Add(new TreeViewNodeCustom(permiso)));
            return nodes;
        }

        private async void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            LoadingService.LoadingStart();
            SelectedUsuario.SetRol((Rol)ListViewPermisos.SelectedItem);
            await (new UsuarioService().Update(SelectedUsuario));
            EditModeEnable = false;
            LoadingService.LoadingStop();
        }


        private async void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await EditMode();
        }

        private async Task EditMode()
        {
            LoadingService.LoadingStart();

            var respose = (await _usuarioRepository.GetUsuarioAsync(SelectedUsuario.Email));
            if (respose.SuccessResult)
            {
                SelectedUsuario = respose.Result;
                EditModeEnable = true;
                var nodeToSelect = Permisos.FirstOrDefault(x => x.Nombre == SelectedUsuario.Rol.Nombre);
                ListViewPermisos.SelectedItem = nodeToSelect;
                PermisoSelected = nodeToSelect;
                LoadingService.LoadingStop();
            }

        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            await EditMode();
        }

    }
}
