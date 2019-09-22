using LicitProd.Data;
using LicitProd.Services;
using LicitProd.UI.Wpf.Views;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace LicitProd.UI.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly Regex validEmailRegex = new Regex(
                                @"^(([^<>()[\]\\.,;:\s@\""]+"
                                + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                + @"[a-zA-Z]{2,}))$",
                                RegexOptions.Compiled);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            new ConcursosRepository().Get()
                           .Success(concursos =>
                           {
                               var a = concursos.ToList()
                                     .Select(x => x.IsValid)
                                     .ToList();
                           });


#if !DEBUG
            if (validEmailRegex.IsMatch(txtEmail.Text))
            {
#endif
            new UsuarioService()
#if DEBUG
                 .Login("german.kuber@outlook.com")
#else
                 .Login(txtEmail.Text, txtPassword.Text)
#endif
                 .Success(usuario =>
                 {
                     new MainView().Show();
                     Hide();
                 })
                 .Error(errors => MessageBox.Show("Los datos ingresados no son correctos.", "Error!"));
#if !DEBUG
            }
            else
            {
                MessageBox.Show("No ingreso un email valido", "Error!");
                return;
            }
#endif
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
