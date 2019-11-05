using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Mappers;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public sealed partial class OfertarConcursoPage : Page
    {
        private int _concursoId;

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
            base.OnNavigatedTo(e);
            AsyncHelper.CallAsyncMethodVoid(() => LoadConcurso(_concursoId));

        }
        private async Task LoadConcurso(int concursoId)
        {
            var concursoService = new ConcursoServices();

            (await concursoService.GetConcursoParaOfertarAsync(concursoId)).Success(x =>
            {
                Concurso = x;
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
                Response<Oferta> result = await new ConcursoServices().RealizarOferta(decimal.Parse(Oferta), Detalle, concurso.Result);

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
