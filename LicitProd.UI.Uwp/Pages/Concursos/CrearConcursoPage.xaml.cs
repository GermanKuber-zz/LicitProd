using LicitProd.Entities;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;
using System.Linq;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

        private void BtnAcept_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadingService.LoadingStart();
            if (decimal.TryParse(Presupuesto, out var parsed)) 
                Concurso.Presupuesto = parsed;
            else
                Concurso.Presupuesto = 0;

            new ConcursoServices().Crear(Concurso)
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
