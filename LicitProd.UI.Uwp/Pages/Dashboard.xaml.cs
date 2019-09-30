using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace LicitProd.UI.Uwp.Pages
{
    public sealed partial class Dashboard : Page
    {
        public ObservableCollection<Concurso> Concursos { get; set; } = new ObservableCollection<Concurso>();
        public string Titulo { get; set; } = "Lalalalallala";
        public Dashboard()
        {
            InitializeComponent();
            LoadingService.LoadingStart();
            LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            (await new ConcursosRepository().Get())
                .Success(concursos =>
                {
                    concursos?.ForEach(x => Concursos.Add(x));
                    LoadingService.LoadingStop();
                });
        }
        public void Group()
        {

        }

    }
}
