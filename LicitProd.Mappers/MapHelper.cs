using LicitProd.Data;
using LicitProd.Entities;
using System;
using System.Data;
using System.Reflection;

namespace LicitProd.Mappers
{
    public static class MapHelper<TEntityType> where TEntityType : IEntityToDb, new()
    {
        public static TEntityType FillObject(TEntityType entity, DataRow row) =>
            ParseObject(row, entity);
        public static TEntityType FillObject(DataRow row) =>
            ParseObject(row, new TEntityType());

        private static TEntityType ParseObject(DataRow row, TEntityType entity)
        {
            var props = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);


            var mapper = ObjectToDbMapperFactory<TEntityType>.Create();

            foreach (var prop in props)
            {
                var columnName = prop.Name;
                mapper.GetColumnName(prop.Name)
                    .Success(x => columnName = x.ColumnName);

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
