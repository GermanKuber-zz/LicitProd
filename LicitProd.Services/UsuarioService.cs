using LicitProd.Entities;
using LicitProd.Seguridad;
using System;
using LicitProd.Data.Repositories;
using System.Threading.Tasks;
using LicitProd.Mappers;

namespace LicitProd.Services
{
    public class UsuarioService : BaseService
    {
        private UsuarioRepository _usuarioRepository = new UsuarioRepository();

        public async Task<Response<Usuario>> LoginAsync(string email, string password) => (await _usuarioRepository
            .GetUsuarioAsync(email, new HashService().Hash(password)))
            .Success(usuario => LoginSuccess(usuario));
        public async Task<Response<Usuario>> LoginAsync(string email) => (await _usuarioRepository
                            .GetUsuarioAsync(email))
                            .Success(usuario => LoginSuccess(usuario));

        public void Logout() =>
               IdentityServices.Instance.RemoveUserLogged();

        private void LoginSuccess(Usuario usuario)
        {
            _usuarioRepository.UpdateLastLoginDate(usuario.Email, DateTime.Now);
            IdentityServices.Instance.SetUserLogged(usuario);

            AsyncHelper.CallAsyncMethod(() => new IdiomasRepository().GetByName("Español"))
                .Success(idioma =>
                    SettingsServices.SetIdioma(idioma));
                

            LogManager.LogInformacion("Login", $"{usuario.Email}");
        }
    }
}
