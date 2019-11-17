using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class ProveedoresRepository : BaseRepository<Proveedor>
    {
        private UsuarioRepository _usuarioRepository = new UsuarioRepository();
        public new Task<Response<List<Proveedor>>> Get() =>
            GetAsync();

        public new async Task<Response<Proveedor>> GetByIdAsync(int id)
        {
            var proveedor = await base.GetByIdAsync(id);
            proveedor.Result.Usuario = (await _usuarioRepository.GetByIdAsync(proveedor.Result.UsuarioId)).Result;
            return proveedor;
        }

        public async Task<Response<Proveedor>> GetByRazonSocial(string razonSocial) =>
            ReturnResult((await GetAsync(new Parameters()
                    .Add("RazonSocial", razonSocial)
                    .Send()))
                .Map(result => Response<Proveedor>.Ok(result.First()),
                    errors => Response<Proveedor>.Error(errors))
                .Success(p => p));
        public async Task<Response<Proveedor>> GetByUserId(int userId) =>
            ReturnResult((await GetAsync(new Parameters()
                    .Add("Usuario_Id", userId)
                    .Send()))
                .Map(result => Response<Proveedor>.Ok(result.First()),
                    errors => Response<Proveedor>.Error(errors))
                .Success(p => p));

    }
}
