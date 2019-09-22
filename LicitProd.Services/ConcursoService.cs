using LicitProd.Data;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class ConcursoService : BaseService
    {
        private ConcursosRepository _concursosRepository = new ConcursosRepository();

        public Response<string> Crear(Concurso concurso)
        {
            _concursosRepository.Insert(concurso);
            return Response<string>.Ok("");
        }
    }
}
