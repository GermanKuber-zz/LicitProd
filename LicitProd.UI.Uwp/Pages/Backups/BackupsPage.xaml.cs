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
        public StorageFile BackupSelected { get; set; }
        public BackupsPage()
        {
            InitializeComponent();
            GetFiles();
        }

        private async Task GetFiles()
        {
            var folder = await StorageFolder.GetFolderFromPathAsync(UserDataPaths.GetDefault().Music);

            StorageFolder assets = await folder.GetFolderAsync("Backups");

            Backups.Clear();
            var files = await assets.GetFilesAsync();
            foreach (var fileToAdd in files)
            {
                Backups.Add(fileToAdd);
            }

        }
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var folder = await StorageFolder.GetFolderFromPathAsync(UserDataPaths.GetDefault().Music);
            LoadingService.LoadingStart();
            StorageFolder assets = await folder.GetFolderAsync("Backups");

            (await new BackupServices().CreateBackup(assets.Path)).Success(x =>
           {
               _pageUtilities.ShowMessageDialog($"El backup {x}, fue creado correctamente");
               GetFiles();
           });
        }
        private async void ButtonRestore_OnClick(object sender, RoutedEventArgs e)
        {
            if (BackupSelected != null)
            {
                LoadingService.LoadingStart();

                (await new BackupServices().RestoreLastBackup(BackupSelected.Path)).Success(x =>
                {
                    _pageUtilities.ShowMessageDialog($"El backup {x}, fue Restaurado correctamente");
                    GetFiles();
                });
            }
        }
    }
}
