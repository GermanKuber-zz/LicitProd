using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace LicitProd.Data.Infrastructure.Extensions
{
    public static class ObservableCollectionsExtensions
    {
        public static int Remove<T>(
            this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }
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
