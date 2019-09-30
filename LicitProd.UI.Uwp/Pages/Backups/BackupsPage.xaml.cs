using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Services;
using LicitProd.UI.Uwp.Pages.Usuarios;
using LicitProd.UI.Uwp.Services;


namespace LicitProd.UI.Uwp.Pages.Backups
{
    public sealed partial class BackupsPage : Page
    {
        private PageUtilities _pageUtilities = new PageUtilities();
        public ObservableCollection<StorageFile> Backups { get; set; } = new ObservableCollection<StorageFile>();

        public BackupsPage()
        {
            InitializeComponent();
            GetFiles();
        }

        private async Task GetFiles()
        {
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Backups");
            
            var files = await assets.GetFilesAsync();
            foreach (var fileToAdd in files)
            {
                Backups.Add(fileToAdd);
            }

        }
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            LoadingService.LoadingStart();
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Backups");
            
            (await new BackupServices().CreateBackup(assets.Path)).Success(x =>
            {
                _pageUtilities.ShowMessageDialog($"El backup {x}, fue creado correctamente");
            });
        }
    }
}
