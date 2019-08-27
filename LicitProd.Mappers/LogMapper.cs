using LicitProd.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LicitProd.Mappers
{
    public static class LogMapper
    {
        public static List<Log> MapList(DataTable dataTable) =>
            dataTable.Rows.ListOfRows().Select(row => MapHelper.FillObject<Log>(row))
                .ToList();

    }

}
