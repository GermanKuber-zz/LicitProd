using System;
using System.Collections.Generic;
using System.Linq;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>
    {

        public UsuarioRepository()
        {
        }

        public new Response<List<Usuario>> Get() =>
                         base.Get();

        public Response<Usuario> GetUsuario(string email, string password) =>
            ReturnResult(Get(new Parameters()
                .Add("Email", email)
                .Add("Password", password)
                .Send())
                .Map(result => Response<Usuario>.Ok(result.First()),
                     errors => Response<Usuario>.Error(errors))
                .Success(usuario =>
                {
                    new RolRepository().GetByUsuarioId(usuario.Id)
                              .Success(x => usuario.SetRol(x));
                    return usuario;
                }));

        public Response<Usuario> GetUsuario(string email) =>
                        ReturnResult(Get(new Parameters()
                            .Add("Email", email)
                            .Send())
                            .Map(result => Response<Usuario>.Ok(result.First()),
                                 errors => Response<Usuario>.Error(errors))
                            .Success(usuario =>
                            {
                                new RolRepository().GetByUsuarioId(usuario.Id)
                                          .Success(x => usuario.SetRol(x));
                                return usuario;
                            }));
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
