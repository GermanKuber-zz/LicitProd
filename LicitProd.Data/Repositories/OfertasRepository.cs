using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class OfertasRepository : BaseRepository<Oferta>
    {

        public async Task<Response<Oferta>> GetByConcursoProveedorId(int id)
        {
          var result =  await GetAsync(new Parameters()
                .Add("Concurso_Proveedor_Id", id)
                .Send());
            return Response<Oferta>.Ok(result.Result?.First());
        }
    }
}
