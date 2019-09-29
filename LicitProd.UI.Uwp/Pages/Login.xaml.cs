using LicitProd.Services;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.UI.Uwp.Services;
using System.Threading.Tasks;
using LicitProd.Entities;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private readonly Regex _validEmailRegex = new Regex(
                                      @"^(([^<>()[\]\\.,;:\s@\""]+"
                                      + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                      + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                      + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                      + @"[a-zA-Z]{2,}))$",
                                      RegexOptions.Compiled);
        public Login()
        {
            this.InitializeComponent();
            //Task.Run(async () => await ValidateConsistency()).Wait();

        }

        public async Task<Response<string>> ValidateConsistency()
        {
            LoadingService.LoadingStart();
            return (await new ConsistencyValidator().Validate())
                .Error(errors =>
               {

                   MessageDialogService.Create("La base de datos se encuentra corrompida. Verifique con el administrador", c =>
                   {
                       LoadingService.LoadingStop();
                       Application.Current.Exit();

                   }, null);
               });
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Loading.IsLoading = true;

            (await ValidateConsistency()).Success(async s =>
            {

#if !DEBUG
                        if (validEmailRegex.IsMatch(txtEmail.Text))
                        {
#endif
                (await new UsuarioService()
#if DEBUG
                             .LoginAsync("german.kuber@outlook.com"))
#else
                             .Login(txtEmail.Text, txtPassword.Text)
#endif
                             .Success(usuario =>
                             {
                                 this.Frame.Navigate(typeof(MainContainerPage));

                             })
                     .Error(errors =>
                     {
                         MessageDialogService.Create("Verifique los datos ingresados!!");
                     });
#if !DEBUG
                        }
                        else
                        {
                            MessageBox.Show("No ingreso un email valido", "Error!");
                            return;
                        }
#endif
            });
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {


        }


    }
}
