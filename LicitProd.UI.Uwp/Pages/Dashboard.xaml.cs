using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using LicitProd.UI.Uwp.Pages.Usuarios;

namespace LicitProd.UI.Uwp.Pages
{
    public sealed partial class Dashboard : Page
    {
        private PageUtilities _pageUtilities = new PageUtilities();
        public ObservableCollection<Concurso> Concursos { get; set; } = new ObservableCollection<Concurso>();
        public Dashboard()
        {
            InitializeComponent();
            //LoadingService.LoadingStart();
            //Task.Run(() => LoadDataAsync()).Wait();
        }
        private async Task LoadDataAsync()
        {
            (await new ConcursosRepository().Get())
                .Success(concursos =>
                {
                    concursos?.ForEach(x => Concursos.Add(x));
                    LoadingService.LoadingStop();
                })
                .Error(erros =>
                {
                    ;
                    _pageUtilities.ShowMessageDialog(erros.First(), () =>
                    {
                        LoadingService.LoadingStop();
                        NavigationService.Close();
                    });
                });
        }
        public void Group()
        {

        }

    }
}
