using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LicitProd.Services;
using LicitProd.UI.Uwp.Pages.Concursos;
using LicitProd.UI.Uwp.Services;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Mappers;
using LicitProd.Seguridad;
using LicitProd.UI.Uwp.Pages.Backups;
using LicitProd.UI.Uwp.Pages.Permisos;
using LicitProd.UI.Uwp.Pages.Proveedores;
using LicitProd.UI.Uwp.Pages.Settings;
using LicitProd.UI.Uwp.Pages.Usuarios;
using TranslationService = LicitProd.Services.TranslationService;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainContainerPage : Page
    {
        public ObservableCollection<string> Idiomas { get; set; } = new ObservableCollection<string>();

        public MainContainerPage()
        {
            InitializeComponent();
            LoadingService.SetLoadingCallBack(() => Loading.IsLoading = true,
                                              () => Loading.IsLoading = false);
            NavigationService.Register(page =>
            {
                contentFrame.Navigate(page, new DrillInNavigationTransitionInfo());
            },
            page =>
            {
                if (contentFrame.CanGoBack)
                    contentFrame.BackStack.Remove(contentFrame.BackStack.ElementAt(contentFrame.BackStack.Count - 1));
                contentFrame.Navigate(page, null, new DrillInNavigationTransitionInfo());

            },
            () =>
            {
                Application.Current.Exit();
            });


            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            IdentityServices.Instance.IsLoggued()
                .Success(x => ApplyPermissions(x.Rol));


        }

        private void NvSample_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                NavigationService.Navigate<SettingsPage>();

            }
            else
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                if (selectedItem != null)
                {
                    switch (selectedItem.Name)
                    {
                        case "Dashboard":
                            NavigationService.Navigate<Dashboard>();
                            break;
                        case "Logs":
                            NavigationService.Navigate<LogsPage>();
                            break;
                        case "CrearConcurso":
                            NavigationService.Navigate<CrearConcurso>();
                            break;
                        case "ListConcursos":
                            NavigationService.Navigate<ListConcursos>();
                            break;
                        case "RegistrarProveedor":
                            NavigationService.Navigate<NewProveedor>();
                            break;
                        case "ListarProveedores":
                            NavigationService.Navigate<ListProveedores>();
                            break;
                        case "AdminPermisos":
                            NavigationService.Navigate<AdminPermisos>();
                            break;
                        case "AdminIdiomas":
                            NavigationService.Navigate<IdiomasSettingsPage>();
                            break;
                        case "BackupsPage":
                            NavigationService.Navigate<BackupsPage>();
                            break;
                        case "IdiomasNewPage":
                            NavigationService.Navigate<IdiomasNewPage>();
                            break;
                        case "UsuariosRolesPage":
                            NavigationService.Navigate<UsuariosRolesPage>();
                            break;
                        case "OfertarConcursoPage":
                            NavigationService.Navigate<OfertarConcursoPage>();
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        public void ApplyPermissions(Rol rol)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new UsuarioService().Logout();
            Frame.Navigate(typeof(Login));
        }

        private void MenuFlyoutItem_OnClick(object sender, RoutedEventArgs e)
        {
            var languaje = ((MenuFlyoutItem)sender).Text;
            AsyncHelper.CallAsyncMethod(() => new IdiomasRepository().GetByName(languaje))
                .Success(idioma =>
                    SettingsServices.SetIdioma(idioma));
        }
    }
}
