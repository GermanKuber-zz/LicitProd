using LicitProd.Data;
using LicitProd.Entities;
using LicitProd.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LicitProd.Services
{
    public class Translation
    {
        public string Key { get; }
        public string Value { get; }

        public Translation(string key, string value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
    public class Translations
    {
        public List<Translation> _translations = new List<Translation>();

        public Translations(List<Translation> translations)
        {
            _translations = translations ?? throw new ArgumentNullException(nameof(translations));
        }

        public string GetTranslation(string key) =>
            _translations.First(x => x.Key == key).Value;
    }
    public static class TranslationService
    {
        private static List<Action<Translations>> _changeLanguageCallback = new List<Action<Translations>>();
        private static Translations _translations;
        public static Response<Translations> GetTranslation()
        {
            if (_translations == null)
                return Response<Translations>.Error();
            return Response<Translations>.Ok(_translations);
        }
        public static void Subscribe(Action<Translations> changeLanguage)
        {
            _changeLanguageCallback.Add(changeLanguage);
        }
        public static void ChangeLanguage(Translations translation)
        {
            _translations = translation;
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
