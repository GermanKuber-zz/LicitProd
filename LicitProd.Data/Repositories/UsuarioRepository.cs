using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>
    {

        public UsuarioRepository()
        {
        }

        public new Task<Response<List<Usuario>>> Get() =>
                         base.GetAsync();

        public async Task<Response<Usuario>> GetUsuarioAsync(string email, string password) =>
            ReturnResult((await GetAsync(new Parameters()
                .Add("Email", email)
                .Add("Password", password)
                .Send()))
                .Map(result => Response<Usuario>.Ok(result.First()),
                     errors => Response<Usuario>.Error(errors))
                .Success(async usuario =>
                {
                    (await new RolRepository().GetByUsuarioIdAsync(usuario.Id))
                              .Success(x => usuario.SetRol(x));
                    return usuario;
                }));


        public async Task<Response<Usuario>> GetUsuarioAsync(string email) =>
                        ReturnResult((await GetAsync(new Parameters()
                            .Add("Email", email)
                            .Send()))
                            .Map(result => Response<Usuario>.Ok(result.First()),
                                 errors => Response<Usuario>.Error(errors))
                            .Success(async usuario =>
                            {
                                (await new RolRepository().GetByUsuarioIdAsync(usuario.Id))
                                          .Success(x => usuario.SetRol(x));
                                return usuario;
                            }));
        public void UpdateLastLoginDate(string email, DateTime date) => SqlAccessService.UpdateDataAsync(new Parameters()
                    .Add("LastLogin", date)
                    .Send(),
                 new Parameters()
                    .Add("Email", email)
                    .Send());



    }
}
