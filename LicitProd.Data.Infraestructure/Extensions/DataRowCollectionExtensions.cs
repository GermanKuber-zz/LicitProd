using System.Collections.Generic;
using System.Data;

namespace LicitProd.Mappers
{
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
