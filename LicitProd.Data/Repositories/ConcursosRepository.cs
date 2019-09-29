using System.Collections.Generic;
using System.Threading.Tasks;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class ConcursosRepository : BaseRepository<Concurso>
    {
        public new async Task<Response<List<Concurso>>> Get()
        {
           return (await GetAsync()).Success(concursos =>
            {
                var finalResponse = Response<List<Concurso>>.Ok(concursos);

                concursos?.ForEach(c =>
                {
                    if (!c.IsValid)
                        finalResponse = Response<List<Concurso>>.Error("Concursos corrompidos");
                });
                return finalResponse;
            });

        }

        public async Task<Concurso> Insert(Concurso concurso)
        {
            await SqlAccessService.InsertDataAsync(concurso);
            await new DigitoVerificadorRepository().Validate<ConcursoVerificable>(
                DigitoVerificadorTablasEnum.Concursos);
            return concurso;
        }
    }
}
