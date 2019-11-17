using LicitProd.Data.Repositories;
using LicitProd.Data.ToDbMapper;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using LicitProd.Seguridad;


namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public class ConcursoParaOfertar
    {
        public Concurso Concurso { get; set; }
        public ConcursoProveedor ConcursoProveedor { get; set; }

        public bool Oferte => ConcursoProveedor?.Oferta != null;
        public ConcursoParaOfertar(Concurso concurso, ConcursoProveedor concursoProveedor)
        {
            Concurso = concurso;
            ConcursoProveedor = concursoProveedor;
        }
    }
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

            (await new ConcursosRepository().GetAsync())
           .Success(concursos =>
           {

               concursos.Where(c => c.Status == (int)ConcursoStatusEnum.Nuevo &&
                                    c.ConcursoProveedores.Any(p => p.Proveedor.UsuarioId == IdentityServices.Instance.GetUserLogged().Id
                                                                   &&
                                                                   p.Status != ProveedorConcursoStatusEnum.Rechazado))?.ToList()?
                   .ForEach(x => Concursos.Add(new ConcursoParaOfertar(x, x.ConcursoProveedores.First(f => f.Proveedor.UsuarioId == IdentityServices.Instance.GetUserLogged().Id))));
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

            if (concurso.ConcursoProveedor.AceptoTerminosYCondiciones)
                if (concurso.Concurso.FechaInicio < DateTime.Now)
                    NavigationService.Navigate<OfertarConcursoPage>(new
                    {
                        ConcursoId = concurso.Concurso.Id,
                        ConcursoProveedorId = concurso.ConcursoProveedor.Id
                    });
                else
                    MessageDialogService.Create("El concurso todavia no se encuentra listo para ofertar");
            else
                MessageDialogService.Create("Usted no acepto los terminos y condiciones generales todavia. Desea acceder a los terminos y condiciones para poder aceptarlos?", c =>
                {
                    NavigationService.Navigate<AceptarTerminosYCondicionesPage>(new
                    {
                        ConcursoId = concurso.Concurso.Id
                    });
                }, x => { });


        }
    }
}
