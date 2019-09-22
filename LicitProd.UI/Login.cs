using FluentAssemblyScanner;
using LicitProd.Data;
using LicitProd.Entities;
using LicitProd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
                     Hide();
                     new MainContainer().Show();
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

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
