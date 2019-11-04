using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using LicitProd.Entities;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicitProd.Data.Repositories;

namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public class ProveedorSelectionViewModel
    {
        public Proveedor Proveedor { get; set; }
        public bool Selected { get; set; }
        public ProveedorSelectionViewModel(Proveedor proveedor)
        {
            Proveedor = proveedor;
        }
    }
    public sealed partial class CrearConcurso : Page
    {
        public ObservableCollection<ProveedorSelectionViewModel> Proveedores { get; set; } = new ObservableCollection<ProveedorSelectionViewModel>();
        private List<Proveedor> ProveedoresToShow { get; set; } = new List<Proveedor>();

        public string Presupuesto { get; set; }
        public Concurso Concurso { get; set; } = new Concurso();

        public DateTimeOffset FechaInicio
        {
            get => DateTime.SpecifyKind((DateTime)Concurso.FechaInicio, DateTimeKind.Local);
            set => Concurso.FechaInicio = ((DateTimeOffset)value).DateTime;
        }

        public DateTimeOffset FechaApertura
        {
            get => DateTime.SpecifyKind((DateTime)Concurso.FechaApertura, DateTimeKind.Local);
            set => Concurso.FechaApertura = ((DateTimeOffset)value).DateTime;
        }


        public CrearConcurso()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            (await new ProveedoresRepository().Get())
           .Success(proveedores =>
           {
               proveedores?.ForEach(x => ProveedoresToShow.Add(x));
               proveedores?.ForEach(x => Proveedores.Add(new ProveedorSelectionViewModel(x)));
               LoadingService.LoadingStop();
           })
           .Error(async x =>
           {
               MessageDialogService.Create("No hay Proveedores", c =>
               {
                   LoadingService.LoadingStop();
                   NavigationService.NavigatePop<Dashboard>();
               }, null);
           });
        }
        private void TxtPresupuesto_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private async void BtnAcept_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadingService.LoadingStart();
            if (decimal.TryParse(Presupuesto, out var parsed))
                Concurso.Presupuesto = parsed;
            else
                Concurso.Presupuesto = 0;

            (await new ConcursoServices().Crear(Concurso, Proveedores.Where(x => x.Selected).Select(x => x.Proveedor).ToList()))
                                    .Success(s =>
                                    {
                                        MessageDialogService.Create("Concurso Creado Existosamente", c =>
                                        {
                                            LoadingService.LoadingStop();
                                            NavigationService.NavigatePop<Dashboard>();
                                        }, null);

                                    })
                                    .Error(erros =>
                                    {

                                        MessageDialogService.Create("Error al Crear el Concoruso", c =>
                                        {
                                            LoadingService.LoadingStop();

                                        }, null);
                                    });
        }
    }
}
