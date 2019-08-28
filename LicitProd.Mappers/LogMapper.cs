using LicitProd.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace LicitProd.Mappers
{
    public  class LogMapper : DbToObjectMapper<Log>
    {
        public Log Map(DataTable dataTable)
        {
            throw new System.NotImplementedException();
        }

        public  List<Log> MapList(DataTable dataTable) =>
            dataTable.Rows.ListOfRows().Select(row => MapHelper.FillObject<Log>(row))
                .ToList();

    }
}
