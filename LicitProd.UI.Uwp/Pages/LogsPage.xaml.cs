using System;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

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
            InitializeComponent();
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
        public void Group()
        {

        }

        private void GroupButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Group();
        }

        private void ButtonBase_OnClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            var folder = StorageFolder.GetFolderFromPathAsync(UserDataPaths.GetDefault().Music).GetAwaiter().GetResult();

            StorageFolder assets = folder.GetFolderAsync("Backups").GetAwaiter().GetResult();

            Document doc = new Document();
            doc.Open();
            doc.Add(new Paragraph("Hello World"));
            doc.Close();

            PdfReader reader = new PdfReader("Chapter1_Example1.pdf");
            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text += PdfTextExtractor.GetTextFromPage(reader, page);
            }
            reader.Close();

            Console.WriteLine(text);

        }

    }
}
