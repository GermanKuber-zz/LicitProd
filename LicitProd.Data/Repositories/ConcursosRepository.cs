using LicitProd.Entities;
using LicitProd.Services;
using System.Collections.Generic;

namespace LicitProd.Data
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
