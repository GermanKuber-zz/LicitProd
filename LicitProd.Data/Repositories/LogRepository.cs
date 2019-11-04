using System.Collections.Generic;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class LogRepository : BaseRepository<Log>
    {
        public void Insertar(Log log, int userId)
        {
            SqlAccessService.InsertDataAsync(log,
                                        new Parameters()
                                             .Add("Usuario_Id", userId));
        }

        public new Task<Response<List<Log>>> Get() =>
            GetAsync();
    }
    public class HitoConcursoRepository : BaseRepository<HitoConcurso>
    {

    }
}
