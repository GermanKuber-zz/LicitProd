using System.Collections.Generic;
using System.Threading.Tasks;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class ConcursosRepository : BaseRepository<Concurso>
    {
        public new Task<Response<List<Concurso>>> Get() =>
            base.GetAsync();
        public Concurso Insert(Concurso concurso)
        {
            SqlAccessService.InsertDataAsync(concurso);
            return concurso;
        }
    }
}
