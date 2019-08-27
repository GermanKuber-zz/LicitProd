using LicitProd.Entities;
using System;
using System.Data;
using System.Reflection;

namespace LicitProd.Mappers
{
    public static class MapHelper
    {
        public static TEntityType FillObject<TEntityType>(TEntityType entity, DataRow row)=>
            ParseObject(row, entity);
        public static TEntityType FillObject<TEntityType>(DataRow row) where TEntityType : new() =>
            ParseObject(row, new TEntityType());

        private static TEntityType ParseObject<TEntityType>(DataRow row, TEntityType entity)
        {
            var props = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var attribute = prop.GetCustomAttribute<DbColumnAttribute>();
                var columnName = prop.Name;

                if (attribute != null)
                    columnName = attribute.Column;

                try
                {
                    prop.SetValue(entity, Convert.ChangeType(row[columnName].ToString(), prop.PropertyType), null);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return entity;
        }
    }
}
