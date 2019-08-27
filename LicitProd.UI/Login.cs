using LicitProd.Data;
using LicitProd.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private readonly Regex validEmailRegex = new Regex(
                                        @"^(([^<>()[\]\\.,;:\s@\""]+"
                                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                        + @"[a-zA-Z]{2,}))$",
                                        RegexOptions.Compiled);

        private void Button1_Click(object sender, EventArgs e)
        {
            var logs = new LogRepository().Get();

            if (validEmailRegex.IsMatch(txtEmail.Text))
            {
                new UsuarioService()
                 .Login(txtEmail.Text, txtPassword.Text)
                 .Success(usuario =>
                 {
                     Hide();
                     new MainContainer().Show();
                 })
                 .Error(errors => MessageBox.Show("Los datos ingresados no son correctos.", "Error!"));            
            }
            else
            {
                MessageBox.Show("No ingreso un email valido", "Error!");
                return;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
