using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Mappers;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;

namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public sealed partial class ConfirmacionGanadorPage : Page
    {
        private int _concursoId;

        public ObservableCollection<ProveedorSelectionViewModel> Proveedores { get; set; } = new ObservableCollection<ProveedorSelectionViewModel>();
        public ObservableCollection<Entities.TerminosYCondiciones> TerminosYCondiciones { get; set; } = new ObservableCollection<Entities.TerminosYCondiciones>();
        public Entities.TerminosYCondiciones TerminosYCondicionesSelected { get; set; }
        private ObservableCollection<ProveedorSelectionViewModel> ProveedoresToShow { get; set; } = new ObservableCollection<ProveedorSelectionViewModel>();
        private ObservableCollection<PreguntaConcurso> Preguntas { get; set; } = new ObservableCollection<PreguntaConcurso>();

        public string Presupuesto { get; set; }
        public Concurso Concurso { get; set; } = new Concurso();
        public ConcursoProveedor ConcursoProveedor { get; set; } = new ConcursoProveedor();

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


        public ConfirmacionGanadorPage()
        {
            InitializeComponent();
        }

        public ConcursoProveedor Ganador { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Concurso = ((dynamic)e.Parameter).Concurso;
            ConcursoProveedor = ((dynamic)e.Parameter).ConcursoProveedor;
            MessageDialogService.Create(
                $"Usted acaba de ser seleccionado como ganador del concurso {Concurso.Nombre}, recibirá toda la información correspondiente por email");

        }
    }
}
