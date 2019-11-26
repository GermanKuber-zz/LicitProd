using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using LicitProd.Entities;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LicitProd.Data.Repositories;
using Windows.UI.Xaml.Navigation;
using LicitProd.Mappers;

namespace LicitProd.UI.Uwp.Pages.Concursos
{

    public sealed partial class DetalleConcursoPage : Page
    {
        private int _concursoId;

        public ObservableCollection<ProveedorSelectionViewModel> Proveedores { get; set; } = new ObservableCollection<ProveedorSelectionViewModel>();
        public ObservableCollection<Entities.TerminosYCondiciones> TerminosYCondiciones { get; set; } = new ObservableCollection<Entities.TerminosYCondiciones>();
        public Entities.TerminosYCondiciones TerminosYCondicionesSelected { get; set; }
        private ObservableCollection<ProveedorSelectionViewModel> ProveedoresToShow { get; set; } = new ObservableCollection<ProveedorSelectionViewModel>();
        private ObservableCollection<PreguntaConcurso> Preguntas { get; set; } = new ObservableCollection<PreguntaConcurso>();

        public string Presupuesto { get; set; }
        public Concurso Concurso { get; set; } = new Concurso();
        public ObservableCollection<ConcursoProveedor> ConcursoProveedores { get; set; } = new ObservableCollection<ConcursoProveedor>();

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


        public DetalleConcursoPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _concursoId = int.Parse(((dynamic)e.Parameter).ConcursoId.ToString());
            base.OnNavigatedTo(e);
            AsyncHelper.CallAsyncMethodVoid(() => LoadConcurso(_concursoId));
            AsyncHelper.CallAsyncMethodVoid(() => LoadDataAsync());


        }
        private async Task LoadConcurso(int concursoId)
        {
            var concursoService = new ConcursoServices();

            (await concursoService.GetConcursoParaOfertarAsync(concursoId)).Success(x =>
            {
                Concurso = x;
                x?.ConcursoProveedores.ForEach(c => ConcursoProveedores.Add(c));

                x?.ConcursoProveedores?.Where(w => w.Pregunta != null)
                                       .Select(s => s?.Pregunta)
                                       .ToList()
                                       ?.ForEach(f => Preguntas.Add(f));
            });
        }

        private async Task LoadDataAsync()
        {
            (await new ProveedoresRepository().Get())
           .Success(proveedores =>
           {
               proveedores?.Where(p => !Concurso.ConcursoProveedores.Any(c => c.ProveedorId == p.Id)).ToList()?
                   .ForEach(x => Proveedores.Add(new ProveedorSelectionViewModel(x)));

               proveedores?.Where(x => Proveedores.Any(s => x.Id == s.Proveedor.Id)).ToList().ForEach(x => ProveedoresToShow.Add(new ProveedorSelectionViewModel(x)));


           })
           .Error(async x =>
           {
               MessageDialogService.Create("No hay Proveedores", c =>
               {
                   LoadingService.LoadingStop();
                   NavigationService.NavigatePop<Dashboard>();
               }, null);
           });

            (await new TerminosYCondicionesRepository().GetAsync())
                .Success(terminos =>
                {
                    terminos.ForEach(x => TerminosYCondiciones.Add(x));
                    TerminosYCondicionesSelected = terminos.FirstOrDefault();
                });
        }
        private void TxtPresupuesto_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private async void BtnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            dataGridProveedoresParaInvitar.IsEnabled = true;
            dtPreguntas.IsEnabled = true;
        }

        private async void BtnAcept_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var proveedoresParaInvitar = ProveedoresToShow.Where(x => x.Selected).Select(x => x.Proveedor).ToList();
            if (proveedoresParaInvitar != null
                &&
                proveedoresParaInvitar.Any())
                MessageDialogService.Create("Una vez invitado los proveedores no podra editarlo, esta seguro que desea continuar?", async c =>
                {
                    if (proveedoresParaInvitar != null
                        &&
                        proveedoresParaInvitar.Any())
                    {
                        await new ConcursoServices().InvitarProveedores(Concurso, proveedoresParaInvitar);
                        LoadingService.LoadingStop();
                    }
                });
            var concursoServices = new ConcursoServices();

            foreach (var preguntaConcurso in Preguntas)
                await concursoServices.ResponderPregunta(preguntaConcurso);


            MessageDialogService.Create("El concurso fue actualziado.", async c =>
            {
                    LoadingService.LoadingStop();
                    NavigationService.NavigatePop<Dashboard>();
            });
        }
    }
}
