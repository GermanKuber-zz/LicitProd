using LicitProd.Entities;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;
using System.Linq;
using Windows.UI.Xaml.Controls;


namespace LicitProd.UI.Uwp.Pages
{
    public sealed partial class CrearConcurso : Page
    {
        public string Presupuesto { get; set; }
        public Concurso Concurso { get; set; } = new Concurso();
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
