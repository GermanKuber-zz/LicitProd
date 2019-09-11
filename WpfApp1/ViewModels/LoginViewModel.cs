using LicitProd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string password;
        private string usuario;

        public string Usuario
        {
            get => usuario; set
            {
                usuario = value;
                SetField(ref usuario, value);
            }
        }
        public string Password
        {
            get => password; set
            {
                password = value;
                SetField(ref password, value);
            }
        }

        public ICommand ConectarCommand {  set; get; }
        private void Conectar() {
            //if (validEmailRegex.IsMatch(txtEmail.Text))
            //{
                new UsuarioService()
                 .Login(Usuario, Password)
                 .Success(usuario =>
                 {
                     //Hide();
                     //new MainContainer().Show();
                     var a = "";
                 })
                 .Error(errors => MessageBox.Show("Los datos ingresados no son correctos.", "Error!"));
            //}
            //else
            //{
            //    MessageBox.Show("No ingreso un email valido", "Error!");
            //    return;
            //}
        }
    }
}
