using LicitProd.Data;
using LicitProd.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

            var log = new LogRepository();
            var logs = log.Get();

            var pass = CryptographyService.Encrypt(this.txtEmail.Text);
            var de = CryptographyService.Decrypt(pass);
            if (!validEmailRegex.IsMatch(this.txtEmail.Text))
            {
                MessageBox.Show("No ingreso un email valido", "Error!");
                return;
            }
            else
            {
                new UsuarioService()
                    .Login(this.txtEmail.Text, this.txtPassword.Text)
                    .Success(usuario =>
                    {
                        Hide();
                        new MainContainer().Show();
                    })
                    .Error(errors => MessageBox.Show("Los datos ingresados no son correctos.", "Error!"));
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
