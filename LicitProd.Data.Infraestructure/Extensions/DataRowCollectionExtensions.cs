using LicitProd.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace LicitProd.Mappers
{

    public static class AttributeExtensions<TAtribute, TEntity> where TEntity : IEntityToDb
                                                       where TAtribute : Attribute
    {
        public static TAtribute GetAttribute()
        {
            var dbTableAttribute = typeof(TEntity).GetCustomAttribute<TAtribute>(false);
            if (dbTableAttribute == null)
                throw new Exception($"La clase {typeof(TEntity).Name} no tiene un atributo DbTableAttribute.");
            return dbTableAttribute;
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
