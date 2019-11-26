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
        private ConcursoProveedor _concursoProveedor;

        public Concurso Concurso { get; set; }
        public string Oferta { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }

        public bool HasRespuesta => !string.IsNullOrWhiteSpace(Respuesta);
        public bool HasPregunta => string.IsNullOrWhiteSpace(Pregunta) && string.IsNullOrWhiteSpace(Oferta);


        public string Detalle { get; set; }
        public DateTimeOffset FechaInicio
        {
            get
            {
                if (Concurso != null)
                    return DateTime.SpecifyKind((DateTime)Concurso.FechaInicio, DateTimeKind.Local);
                return DateTimeOffset.Now;
            }
            set => Concurso.FechaInicio = value.DateTime;
        }

        public DateTimeOffset FechaApertura
        {
            get
            {
                if (Concurso != null)
                    return DateTime.SpecifyKind((DateTime)Concurso.FechaApertura, DateTimeKind.Local);
                return DateTimeOffset.Now;
            }
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
                    _concursoProveedor = Concurso.ConcursoProveedores.First(x => x.Id == _concursoProveedorId);
                    var oferta = _concursoProveedor.Oferta;
                    Pregunta = _concursoProveedor?.Pregunta?.Pregunta;
                    Respuesta = _concursoProveedor?.Pregunta?.Respuesta;
                    if (oferta != null)
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
            if ((!string.IsNullOrWhiteSpace(Oferta)
                &&
                !string.IsNullOrWhiteSpace(Detalle)))
            {
                LoadingService.LoadingStart();
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
            else if (!string.IsNullOrWhiteSpace(Pregunta) && string.IsNullOrWhiteSpace(Respuesta))
            {
                LoadingService.LoadingStart();
                var result = await new ConcursoServices().HacerPregunta(_concursoProveedor, Pregunta);
                result.Success(x =>
                {
                    MessageDialogService.Create("Acaba de realizar una pregunta, sera notificado cuando el comprador le de una respuesta", c =>
                    {
                        LoadingService.LoadingStop();
                        NavigationService.Navigate<ListConcursosOfertarPage>();
                    }, null);
                });

            }
        }
    }
}
