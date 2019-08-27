using LicitProd.Entities;
using LicitProd.Mappers;
using System.Collections.Generic;

namespace LicitProd.Data
{
    public class LogRepository : BaseRepository
    {
        public void Insertar(Log log, int userId)
        {
            SqlAccessService.InsertData("Logs",
              new Parameters()
             .Add("Nombre", log.Nombre)
             .Add("Descripcion", log.Descripcion)
             .Add("Type", log.Type.ToString())
             .Add("Fecha", log.Fecha)
             .Add("Usuario_Id", userId)
             .Send());
        }
        public List<Log> Get()=>
            LogMapper.MapList(SqlAccessService.SelectData("Logs",
                 EntityToColumns<Log>.Map()
                .Send()));


    }
}
