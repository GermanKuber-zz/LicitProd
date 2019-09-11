using LicitProd.Data;
using LicitProd.Entities;
using LicitProd.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public partial class Login : BaseForm
    {
        public Login() : base()
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
            if (validEmailRegex.IsMatch(txtEmail.Text))
            {
                new UsuarioService()
#if DEBUG
                 .Login(txtEmail.Text)
#else
                 .Login(txtEmail.Text, txtPassword.Text)
#endif
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
