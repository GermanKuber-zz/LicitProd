using LicitProd.Data;
using LicitProd.Entities;
using LicitProd.Seguridad;

namespace LicitProd.Services
{
    public class UsuarioService : BaseService
    {

        public Response<Usuario> Login(string email, string password) =>
            new UsuarioRepository().GetUsuario(email, new HashService()
                                                            .Hash(password))
            .Success(usuario => LoginSuccess(usuario));

        public void Logout() =>
               IdentityServices.RemoveUserLogged();

        private void LoginSuccess(Usuario usuario)
        {
            IdentityServices.SetUserLogged(usuario);
            LogManager.LogInformacion("Login", $"{usuario.Email}");
        }
    }
}
