using System.Collections.Generic;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class LogRepository : BaseRepository<Log>
    {
        public void Insertar(Log log, int userId)
        {
            SqlAccessService.InsertData(log,
                                        new Parameters()
                                             .Add("Usuario_Id", userId));
        }

        public new Response<List<Log>> Get() =>
            base.Get();
    }
}
