using LicitProd.Data;
using LicitProd.Entities;
using LicitProd.Seguridad;
using System;

namespace LicitProd.Services
{
    public class UsuarioService : BaseService
    {
        private UsuarioRepository _usuarioRepository = new UsuarioRepository();

        public Response<Usuario> Login(string email, string password) => _usuarioRepository
            .GetUsuario(email, new HashService().Hash(password))
            .Success(usuario => LoginSuccess(usuario));

        public void Logout() =>
               IdentityServices.RemoveUserLogged();

        private void LoginSuccess(Usuario usuario)
        {
            _usuarioRepository.UpdateLastLoginDate(usuario.Email, DateTime.Now);
            IdentityServices.SetUserLogged(usuario);
            LogManager.LogInformacion("Login", $"{usuario.Email}");
        }
    }
}
