using System.Collections.Generic;
using System.Threading.Tasks;
using LicitProd.Entities;
using LicitProd.Infrastructure;

namespace LicitProd.Data.Repositories
{
    public class ConcursosRepository : BaseRepository<Concurso>
    {
        public new Task<Response<List<Concurso>>> Get() =>
            base.GetAsync();
        public async Task<Concurso> Insert(Concurso concurso)
        {
            await SqlAccessService.InsertDataAsync(concurso);
            await new DigitoVerificadorRepository().Validate<ConcursoVerificable>(
                DigitoVerificadorTablasEnum.Concursos);
            return concurso;
        }
    }
}
