using LicitProd.Data.Repositories;
using LicitProd.Data.ToDbMapper;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;


namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public sealed partial class ListConcursosOfertarPage : Page
    {
        public ObservableCollection<ConcursoParaOfertar> Concursos { get; set; } = new ObservableCollection<ConcursoParaOfertar>();
        public ListConcursosOfertarPage()
        {
            InitializeComponent();
            LoadingService.LoadingStart();
            LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {

            (await new ConcursosRepository().GetToOfertar())
           .Success(concursos =>
           {
               concursos?.ForEach(x => Concursos.Add(x));
               LoadingService.LoadingStop();
           })
           .Error(async x =>
           {
               MessageDialogService.Create("No hay concursos para mostrar", c =>
               {
                   LoadingService.LoadingStop();
               }, null);
           });
        }
        public void Group()
        {

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var concurso = (ConcursoParaOfertar)e.AddedItems.First();

            if (concurso.AceptoTerminosYCondiciones)
                if (concurso.FechaInicio < DateTime.Now)
                    NavigationService.Navigate<OfertarConcursoPage>(new { ConcursoId = concurso.Id });
                else
                    MessageDialogService.Create("El concurso todavia no se encuentra listo para ofertar");
            else
                MessageDialogService.Create("Usted no acepto los terminos y condiciones generales todavia. Desea acceder a los terminos y condiciones para poder aceptarlos?", c =>
                {
                    NavigationService.Navigate<AceptarTerminosYCondicionesPage>(new { ConcursoId = concurso.Id });
                }, x => { });


        }
    }
}
