using LicitProd.Entities;
using LicitProd.Mappers;
using LicitProd.Services;
using System;
using System.Linq;

namespace LicitProd.Data
{
    public class UsuarioRepository : BaseRepository<Usuario>
    {

        public UsuarioRepository()
        {
        }
        public Response<Usuario> GetUsuario(string email, string password) =>
            Get(new Parameters()
                .Add("Email", email)
                .Add("Password", password)
                .Send())
                .Map(result => Response<Usuario>.Ok(result.First()),
                     errors => Response<Usuario>.Error(errors));


        public void UpdateLastLoginDate(string email, DateTime date) => SqlAccessService.UpdateData(new Parameters()
                    .Add("LastLogin", date)
                    .Send(),
                 new Parameters()
                    .Add("Email", email)
                    .Send());

        public void InsertUsuario(Usuario usuario) =>
            SqlAccessService.InsertData(new Parameters()
                .Add("Email", usuario.Email)
                .Add("Password", usuario.HashPassword)
                .Send());

    }
}
