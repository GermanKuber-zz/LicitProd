using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Mappers;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public sealed partial class OfertarConcursoPage : Page
    {
        private int _concursoId;
        private int _concursoProveedorId;

        public Concurso Concurso { get; set; }
        public string Oferta { get; set; }
        public string Detalle { get; set; }
        public DateTimeOffset FechaInicio
        {
            get => DateTime.SpecifyKind((DateTime)Concurso.FechaInicio, DateTimeKind.Local);
            set => Concurso.FechaInicio = value.DateTime;
        }

        public DateTimeOffset FechaApertura
        {
            get => DateTime.SpecifyKind((DateTime)Concurso.FechaApertura, DateTimeKind.Local);
            set => Concurso.FechaApertura = ((DateTimeOffset)value).DateTime;
        }
        public OfertarConcursoPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _concursoId = int.Parse(((dynamic)e.Parameter).ConcursoId.ToString());
            _concursoProveedorId = int.Parse(((dynamic)e.Parameter).ConcursoProveedorId.ToString());
            base.OnNavigatedTo(e);
            AsyncHelper.CallAsyncMethodVoid(() => LoadConcurso(_concursoId));

        }
        private async Task LoadConcurso(int concursoId)
        {
            var concursoService = new ConcursoServices();
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                AsyncHelper.CallAsyncMethod(() => concursoService.GetConcursoParaOfertarAsync(concursoId)).Success(t =>
                {
                    Concurso = t;
                    var oferta = Concurso.ConcursoProveedores.First(x => x.Id == _concursoProveedorId).Oferta;
                    if ( oferta!= null)
                    {
                        MessageDialogService.Create("Ya tiene una oferta realizada. Solo podrá ver su detalle, pero no podrá realizar ninguna modificación en la misma.");

                        txtOferta.IsEnabled = false;
                        Oferta = oferta.Monto.ToString();
                        txtDescripcionOferta.IsEnabled = false;
                        Detalle = oferta.Detalle.ToString();

                        BtnOfertar.Visibility = Visibility.Collapsed;
                    }
                });
            });
        }
        private async void BtnAcept_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Oferta)
                ||
                string.IsNullOrWhiteSpace(Detalle))
                MessageDialogService.Create("Debe indicar una oferta valida");
            else
            {
                LoadingService.LoadingStart();
                var concurso = await new ConcursosRepository().GetByIdAsync(_concursoId);
                Response<Oferta> result = await new ConcursoServices().RealizarOferta(decimal.Parse(Oferta), Detalle, _concursoProveedorId);

                result.Success(x =>
                {
                    MessageDialogService.Create("Su oferta se realizo correctamente", c =>
                    {
                        LoadingService.LoadingStop();
                        NavigationService.Navigate<ListConcursosOfertarPage>();
                    }, null);
                });


            }
        }
    }
}
