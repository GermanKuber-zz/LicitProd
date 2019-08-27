using LicitProd.Entities;
using System;
using System.Data;
using System.Reflection;

namespace LicitProd.Mappers
{
    public static class MapHelper
    {
        public static TEntityType FillObject<TEntityType>(TEntityType entity, DataRow row) =>
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
                    var value = row[columnName].ToString();
                    if (prop.PropertyType.IsEnum)
                        prop.SetValue(entity, Enum.Parse(prop.PropertyType, value));
                    else
                        prop.SetValue(entity, Convert.ChangeType(value, prop.PropertyType), null);
                }
                catch (ArgumentException ex)
                {
                    //TODO : Extrae el try catch
                    Console.WriteLine(ex);
                }
                catch (Exception) { }
            }
            return entity;
        }
    }

}
