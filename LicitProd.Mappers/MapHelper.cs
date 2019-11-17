using LicitProd.Entities;
using System;
using System.Data;
using LicitProd.Data.Infrastructure;
using LicitProd.Infrastructure;
using System.Threading.Tasks;

namespace LicitProd.Mappers
{
    public static class MapHelper<TEntityType> where TEntityType : IEntityToDb, new()
    {
        public static async Task<TEntityType> FillObjectAsync(TEntityType entity, DataRow row) =>
            await ParseObjectAsync(row, entity);
        public static async Task<TEntityType> FillObjectAsync(DataRow row) =>
            await ParseObjectAsync(row, new TEntityType());

        private static async Task<TEntityType> ParseObjectAsync(DataRow row, TEntityType entity)
        {
            var mapper = await ObjectToDbMapperFactory<TEntityType>.Create();

            foreach (var prop in ReflectionHelper.GetListOfProperties<TEntityType>())
            {
                var columnName = prop.Name;
                mapper.GetColumnName(prop.Name)
                    .Success(x =>
                    {
                        if (!string.IsNullOrEmpty(x.ColumnName))
                            columnName = x.ColumnName;
                    });

                try
                {
                    if (row.Table.Columns[columnName] != null)
                    {
                        var value = row[columnName].ToString();
                        if (prop.PropertyType.IsEnum && !string.IsNullOrWhiteSpace(value))
                            prop.SetValue(entity, Enum.Parse(prop.PropertyType, value));
                        else if (!prop.PropertyType.IsEnum)
                            prop.SetValue(entity, Convert.ChangeType(value, prop.PropertyType), null);}
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
