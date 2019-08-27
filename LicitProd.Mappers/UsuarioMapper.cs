using LicitProd.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace LicitProd.Mappers
{
    public static class UsuarioMapper
    {
        public static Usuario Map(DataTable dataTable) =>
            MapHelper.FillObject<Usuario>(dataTable.Rows[0]);
    }
    public static class LogMapper
    {
        public static List<Log> Map(DataTable dataTable) =>
            dataTable.Rows.ListOfRows().Select(row => MapHelper.FillObject<Log>(row))
                .ToList();
            
    }

    public static class DataRowCollectionExtensions
    {
        public static List<DataRow> ListOfRows(this DataRowCollection dataRow)
        {
            var listToReturn = new List<DataRow>();
            foreach (DataRow item in dataRow)
                listToReturn.Add(item);
            return listToReturn;
        }
    }

}
