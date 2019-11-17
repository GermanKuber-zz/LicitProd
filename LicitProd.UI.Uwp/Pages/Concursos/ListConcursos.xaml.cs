using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using LicitProd.Seguridad;


namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public sealed partial class ListConcursos : Page
    {
        public ObservableCollection<Concurso> Concursos { get; set; } = new ObservableCollection<Concurso>();
        public ListConcursos()
        {
            InitializeComponent();
            LoadingService.LoadingStart();
            LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            
            (await new ConcursosRepository().GetAsync())
           .Success(concursos =>
           {
               concursos?.Where(x=> x.Comprador.UsuarioId == IdentityServices.Instance.GetUserLogged().Id)
                   .ToList()
                   ?.ForEach(x=> Concursos.Add(x));
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

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var concurso = (Concurso)e.AddedItems.First();
            NavigationService.Navigate<DetalleConcursoPage>(new { ConcursoId = concurso.Id });
        }
    }
}
