using LicitProd.Data;
using LicitProd.Entities;
using LicitProd.Seguridad;
using System;
using System.Collections.Generic;

namespace LicitProd.Services
{
    public static class TranslationService
    {
        private static List<Action<ITranslation>> _changeLanguageCallback = new List<Action<ITranslation>>();

        public static void Subscribe(Action<ITranslation> changeLanguage)
        {
            _changeLanguageCallback.Add(changeLanguage);
        }
        public static void ChangeLanguage(ITranslation translation)
        {
            _changeLanguageCallback.ForEach(callback => callback(translation));
        }
    }
    public class UsuarioService : BaseService
    {
        private UsuarioRepository _usuarioRepository = new UsuarioRepository();

        public Response<Usuario> Login(string email, string password) => _usuarioRepository
            .GetUsuario(email, new HashService().Hash(password))
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
