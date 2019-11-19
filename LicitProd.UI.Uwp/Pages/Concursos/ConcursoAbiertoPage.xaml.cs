using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Mappers;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages.Concursos
{

    public class OfertaViewModel
    {
        public ConcursoProveedor ConcursoProveedor { get; }
        public bool Selected
        {
            get { return ConcursoProveedor.Ganador; }
            set { }
        }
        public OfertaViewModel(ConcursoProveedor concursoProveedor)
        {
            ConcursoProveedor = concursoProveedor;
        }
    }

    public sealed partial class ConcursoAbiertoPage : Page
    {

        private ObservableCollection<PreguntaConcurso> Preguntas { get; } = new ObservableCollection<PreguntaConcurso>();
        public bool ParaEditar { get; set; } = true;
        public Concurso Concurso { get; set; } = new Concurso();
        public ObservableCollection<ConcursoProveedor> ConcursoProveedores { get; set; } = new ObservableCollection<ConcursoProveedor>();
        public ObservableCollection<OfertaViewModel> Ofertas { get; set; } = new ObservableCollection<OfertaViewModel>();
        public string Presupuesto
        {
            get { return Concurso?.Presupuesto.ToString(); }
            set { }
        }

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


        public ConcursoAbiertoPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Concurso = ((dynamic)e.Parameter).Concurso.Concurso;
            Concurso.ConcursoProveedores.Where(x => x.Oferta != null).ToList()?.ForEach(x => Ofertas.Add(new OfertaViewModel(x)));
            base.OnNavigatedTo(e);
            if (Concurso.Status == (int)ConcursoStatusEnum.Cerrado)
                ParaEditar = false;
            else
            {
                Concurso.Status = (int)ConcursoStatusEnum.Abierto;
                AsyncHelper.CallAsyncMethodVoid(() => new ConcursosRepository().UpdateDataAsync(Concurso));
            }

            if (Concurso.AdjudicacionDirecta && Concurso.Status != (int)ConcursoStatusEnum.Cerrado)
            {
                var ganador = Concurso.ConcursoProveedores.Where(x => x.Oferta != null).OrderByDescending(x => x.Oferta.Monto).FirstOrDefault();
                if (ganador != null)
                    MessageDialogService.Create($"Este concurso esta marcado como adjudicación directa, por lo que el ganador es : {ganador.Proveedor.RazonSocial}", c =>
                    {
                        AsyncHelper.CallAsyncMethodVoid(() => new ConcursoServices().EstablecerGanador(Concurso, ganador));
                        NavigationService.NavigatePop<Dashboard>();
                    }, null);
                else
                    MessageDialogService.Create($"Debe seleccionar un ganador.");
            }


        }


        private async Task LoadDataAsync()
        {

        }
        private void TxtPresupuesto_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private async void BtnEditar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private async void BtnAcept_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataGridProveedoresParaInvitar.SelectedItem == null)
                MessageDialogService.Create($"Debe seleccionar un ganador.");
            else
            {
                var ganador = (ConcursoProveedor)dataGridProveedoresParaInvitar.SelectedItem;
                if (ganador == null)
                    await SetGanador(ganador);
            }
        }

        private async Task SetGanador(ConcursoProveedor ganador)
        {
            if (ganador != null)
                MessageDialogService.Create($"Acaba de seleccionar a {ganador.Proveedor.RazonSocial} como ganador. Si confirma el ganador precione aceptar, recuerde que una vez seleccionado el ganador, el concurso estara cerrado y no podra manipularlo mas.", c =>
                {
                    AsyncHelper.CallAsyncMethodVoid(() => new ConcursoServices().EstablecerGanador(Concurso, ganador));
                    NavigationService.NavigatePop<Dashboard>();
                }, null);
            else
                MessageDialogService.Create($"Debe seleccionar un ganador.");

        }
    }
}
