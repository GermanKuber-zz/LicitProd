using LicitProd.Data.Repositories;
using LicitProd.Mappers;
using LicitProd.Seguridad;
using LicitProd.Services;
using LicitProd.UI.Uwp.Services;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages.Concursos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AceptarTerminosYCondicionesPage : Page
    {
        public Entities.TerminosYCondiciones TerminosYCondiciones { get; set; }
        private int _concursoId;
        public AceptarTerminosYCondicionesPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _concursoId = int.Parse(((dynamic)e.Parameter).ConcursoId.ToString());
            base.OnNavigatedTo(e);
            AsyncHelper.CallAsyncMethodVoid(() => LoadConcurso());

        }
        private async Task LoadConcurso()
        {
            var terminosYCondicionesRepository = new TerminosYCondicionesRepository();

            (await terminosYCondicionesRepository.GetByConcurdoId(_concursoId)).Success(x =>
            {
                TerminosYCondiciones = x;
            });
        }
        private async void BtnAcept_Click(object sender, RoutedEventArgs e)
        {
            var userId = IdentityServices.Instance.GetUserLogged().Id;

            await new ConcursoServices().AceptarTerminosYCondiciones(_concursoId, userId);

            MessageDialogService.Create("Gracias por aceptar los terminos y condiciones, ahora puede realiar su oferta.", c =>
            {
                NavigationService.Navigate<ListConcursosOfertarPage>();
            }, x => { });

        }
    }
}
