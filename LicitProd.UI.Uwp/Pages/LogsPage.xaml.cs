using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogsPage : Page
    {
        public ObservableCollection<Log> Logs { get; set; } = new ObservableCollection<Log>();
        public LogsPage()
        {
            this.InitializeComponent();
            LoadingService.LoadingStart();
            LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            (await new LogRepository().Get())
           .Success(logs =>
           {
               logs.ForEach(x => Logs.Add(x));
               LoadingService.LoadingStop();
           })
           .Error(async x =>
           {
               var dialog = new MessageDialog("No hay logs");
               dialog.ShowAsync();
               LoadingService.LoadingStop();

           });
        }
        public void Group() {
        
        }

        private void GroupButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Group();
        }
    }
}
