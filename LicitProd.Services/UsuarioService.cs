using LicitProd.Data;
using LicitProd.Entities;
using LicitProd.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LicitProd.Services
{
    public static class VerificableService
    {
        public static Response<string> Verificar(IList<Verificable> verificables)
        {
            if (verificables.ToList()
                        .Select(x => x.IsValid)
                        .Any(x => !x))
                return Response<string>.Ok("");
            return Response<string>.Error();
        }
    }
    public class UsuarioService : BaseService
    {
        private UsuarioRepository _usuarioRepository = new UsuarioRepository();

        public Response<Usuario> Login(string email, string password) => _usuarioRepository
            .GetUsuario(email, new HashService().Hash(password))
            .Success(usuario => LoginSuccess(usuario));
        public Response<Usuario> Login(string email) => _usuarioRepository
                            .GetUsuario(email)
                            .Success(usuario => LoginSuccess(usuario));

        public void Logout() =>
               IdentityServices.Instance.RemoveUserLogged();

        private void LoginSuccess(Usuario usuario)
        {
            _usuarioRepository.UpdateLastLoginDate(usuario.Email, DateTime.Now);
            IdentityServices.Instance.SetUserLogged(usuario);
            LogManager.LogInformacion("Login", $"{usuario.Email}");
        }
    }
}
