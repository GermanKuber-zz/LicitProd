using LicitProd.Entities;
using LicitProd.Seguridad;
using System;
using System.Linq;
using LicitProd.Data.Repositories;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Data.ToDbMapper;
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

        public async Task Register(Usuario usuario)
        {
            var usuarioRepository = new UsuarioRepository();
            var response = await usuarioRepository.InsertDataAsync(usuario);

            //if (response.SuccessResult)
            //{
            //    var toUpdate = response.Result.First();
            //    toUpdate.RolId = usuario.Rol.Id;
            //    await usuarioRolRepository.UpdateDataAsync(response.Result.First());
            //}
        }

        public async Task<Response<Usuario>> Update(Usuario usuario)
        {
            return await _usuarioRepository.UpdateDataAsync(usuario);
        }
    }
}
