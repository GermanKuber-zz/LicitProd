using LicitProd.UI.Uwp.Pages.Concursos;
using LicitProd.UI.Uwp.Services;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainContainerPage : Page
    {

        public MainContainerPage()
        {
            this.InitializeComponent();
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
                contentFrame.Navigate(page, new DrillInNavigationTransitionInfo());

            });

        }

        private void NvSample_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(Login));
            }
            else
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                if (selectedItem != null)
                {
                    switch (selectedItem.Name)
                    {
                        case "Dashboard":
                            NavigationService.Navigate<LogsPage>();
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
                        default:
                            break;
                    }
                }
            }
        }
    }
}
