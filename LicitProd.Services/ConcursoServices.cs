using LicitProd.Data.Repositories;
using LicitProd.Entities;
using System.Threading.Tasks;

namespace LicitProd.Services
{
    public class ConcursoServices : BaseService
    {
        private ConcursosRepository _concursosRepository = new ConcursosRepository();

        public async Task<Response<string>> Crear(Concurso concurso)
        {
            await _concursosRepository.Insert(concurso);
            return Response<string>.Ok("");
        }
    }
}
