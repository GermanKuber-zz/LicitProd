using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using System.Collections.ObjectModel;
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
            var concursoResponse = (await new ConcursosRepository().Get());
           if (concursoResponse.SuccessResult)
            {
                concursoResponse.Result?.ForEach(x => Concursos.Add(x));
                LoadingService.LoadingStop();

            }
           else
           {
               MessageDialogService.Create("No hay concursos", c =>
               {
                   LoadingService.LoadingStop();
                   NavigationService.NavigatePop<Dashboard>();
               }, null);
            }
        }
        public void Group()
        {

        }

    }
}
