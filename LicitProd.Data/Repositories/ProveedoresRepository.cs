using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class ProveedoresRepository : BaseRepository<Proveedor>
    {
        public new Task<Response<List<Proveedor>>> Get() =>
            base.GetAsync();


        public async Task<Response<Proveedor>> GetByRazonSocial(string razonSocial) =>
            ReturnResult((await GetAsync(new Parameters()
                    .Add("RazonSocial", razonSocial)
                    .Send()))
                .Map(result => Response<Proveedor>.Ok(result.First()),
                    errors => Response<Proveedor>.Error(errors))
                .Success(p => p));
    }
}
