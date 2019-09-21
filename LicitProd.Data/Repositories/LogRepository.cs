using LicitProd.Entities;
using LicitProd.Services;
using System.Collections.Generic;

namespace LicitProd.Data
{
    public class LogRepository : BaseRepository<Log>
    {
        public void Insertar(Log log, int userId)
        {
            SqlAccessService.InsertData(log);
        }

        public new Response<List<Log>> Get() =>
            base.Get();
    }
}
