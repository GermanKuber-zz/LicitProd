using System.Collections.Generic;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class ConcursosRepository : BaseRepository<Concurso>
    {
        public new Response<List<Concurso>> Get() =>
            base.Get();
        public Concurso Insert(Concurso concurso)
        {
            SqlAccessService.InsertData(concurso);
            return concurso;
        }
    }
}
