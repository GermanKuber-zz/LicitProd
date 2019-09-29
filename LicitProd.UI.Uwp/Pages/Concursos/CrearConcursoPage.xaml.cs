using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using LicitProd.Entities;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;

namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public sealed partial class CrearConcurso : Page
    {
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
            this.InitializeComponent();
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

            (await new ConcursoServices().Crear(Concurso))
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
