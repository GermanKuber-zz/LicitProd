using LicitProd.Entities;
using System.Threading.Tasks;

namespace LicitProd.Data.Repositories
{
    public class TerminosYCondicionesRepository : BaseRepository<TerminosYCondiciones>
    {

        public async Task<Response<TerminosYCondiciones>> GetByConcurdoId(int concursoId)
        {

            var concurso = await (new ConcursosRepository().GetByIdAsync(concursoId));

            var result = await GetByIdAsync(concurso.Result.TerminosYCondicionesId);
            return result;
        }
    }
}