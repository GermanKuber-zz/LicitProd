using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;


namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public sealed partial class ListConcursos : Page
    {
        public ObservableCollection<Concurso> Concursos { get; set; } = new ObservableCollection<Concurso>();
        public ListConcursos()
        {
            this.InitializeComponent();
            LoadingService.LoadingStart();
            LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            (await new ConcursosRepository().Get())
           .Success(concursos =>
           {
               concursos?.ForEach(x=> Concursos.Add(x));
               LoadingService.LoadingStop();
           })
           .Error(async x =>
           {
               MessageDialogService.Create(x.First(), c =>
               {
                   LoadingService.LoadingStop();
                   NavigationService.Close();
               }, null);
           });
        }
        public void Group()
        {

        }

    }
}
