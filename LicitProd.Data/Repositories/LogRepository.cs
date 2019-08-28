using LicitProd.Entities;
using LicitProd.Services;
using System.Collections.Generic;

namespace LicitProd.Data
{
    public class LogRepository : BaseRepository<Log>
    {
        public void Insertar(Log log, int userId)
        {
            SqlAccessService.InsertData(
              new Parameters()
                 .Add("Nombre", log.Nombre)
                 .Add("Descripcion", log.Descripcion)
                 .Add("Type", log.Type.ToString())
                 .Add("Fecha", log.Fecha)
                 .Add("Usuario_Id", userId)
                 .Send());
        }

        public new Response<List<Log>> Get()=>
            base.Get();
    }
}
