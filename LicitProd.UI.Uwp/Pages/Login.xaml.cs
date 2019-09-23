using System.Linq;
using LicitProd.Services;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LicitProd.Data.Repositories;
using LicitProd.Infrastructure;

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
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainContainerPage), null);
            //            new ConcursosRepository().Get()
            //                .Success(concursos =>
            //                {
            //                    var a = concursos.ToList()
            //                          .Select(x => x.IsValid)
            //                          .ToList();
            //                });


            //#if !DEBUG
            //            if (validEmailRegex.IsMatch(txtEmail.Text))
            //            {
            //#endif
            //            new UsuarioService()
            //#if DEBUG
            //                 .Login("german.kuber@outlook.com")
            //#else
            //                 .Login(txtEmail.Text, txtPassword.Text)
            //#endif
            //                 .Success(usuario =>
            //                 {
            //                 })
            //                 .Error(errors => {
            //                 });
            //#if !DEBUG
            //            }
            //            else
            //            {
            //                MessageBox.Show("No ingreso un email valido", "Error!");
            //                return;
            //            }
            //#endif
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {


        }
    }
}
