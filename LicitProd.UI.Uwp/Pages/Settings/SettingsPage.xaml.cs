using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Seguridad;
using LicitProd.UI.Uwp.Controls;
using LicitProd.UI.Uwp.Pages.Usuarios;
using LicitProd.UI.Uwp.Services;


namespace LicitProd.UI.Uwp.Pages.Settings
{
    public sealed partial class SettingsPage : Page
    {
        private PageUtilities _pageUtilities = new PageUtilities();
        private string _themeName;
        private Configuracion _configuracion;
        private Idioma _idiomaSelected;
        private readonly ConfiguracionesRepository _configuracionesRepository;
        public SettingsPage()
        {
            InitializeComponent();
            LoadData();
            _configuracionesRepository = new ConfiguracionesRepository();

        }

        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
        {


        }

        private async Task LoadData()
        {
            (await new IdiomasRepository().GetIdiomaAlone())
                .Success(idiomas =>
                {
                    idiomas.ForEach(s =>
                    {
                        var radio = new RadioButtonCustom();

                        radio.Content = s.Nombre;
                        radio.DataContext = s;
                        radio.Click += RadioOnClick;
                        IdiomaPanel.Children.Add(radio);

                    });
                });

            (await _configuracionesRepository.GetAsync())
                .Success(x =>
                {
                    _configuracion = x.First();
                    SetSettings(_configuracion);
                });

        }

        private void CheckThee()
        {
            if (RbDark.IsChecked.Value)
                _themeName = "Dark";
            if (RbDark.IsChecked.Value)
                _themeName = "Light";
            if (RbDark.IsChecked.Value)
                _themeName = "RbLight";
        }
        private void SetSettings(Configuracion settings)
        {
            switch (settings.Theme)
            {
                case "Dark":
                    RbDark.IsChecked = true;
                    break;
                case "Light":
                    RbLight.IsChecked = true;
                    break;
                case "Default":
                    RbDefault.IsChecked = true;
                    break;
            }
            IdiomaPanel.Children.ToList().ForEach(x =>
            {
                var control = ((RadioButtonCustom)x);
                if (((Idioma)control.DataContext).Id == settings.IdiomaId)
                    control.IsChecked = true;
            });
        }

        private void RadioOnClick(object sender, RoutedEventArgs e)
        {
            _idiomaSelected = (Idioma)((RadioButtonCustom)sender).DataContext;
        }

        private void OnFeedbackButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void soundToggle_Toggled(object sender, RoutedEventArgs e)
        {

        }


        private async void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            LoadingService.LoadingStart();
            CheckThee();
            if (_configuracion == null)
                (await _configuracionesRepository.InsertDataAsync(new Configuracion
                {
                    IdiomaId = _idiomaSelected.Id,
                    Theme = _themeName,
                    UsuarioId = IdentityServices.Instance.GetUserLogged().Id
                }))
                    .Success(x =>
                    {
                        _pageUtilities.ShowMessageDialog("Configuración Actualizada correctamente");
                    });
            else
            {
                _configuracion.IdiomaId = _idiomaSelected.Id;
                _configuracion.Theme = _themeName;
                (await _configuracionesRepository.UpdateDataAsync(_configuracion))
                    .Success(x =>
                    {
                        _pageUtilities.ShowMessageDialog("Configuración Actualizada correctamente");

                    });
            }

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _themeName = ((RadioButton)sender).Tag as string;

        }
    }
}
