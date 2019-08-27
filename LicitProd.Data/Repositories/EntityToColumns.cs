using LicitProd.Entities;
using System.Reflection;

namespace LicitProd.Data
{
    public static class EntityToColumns<TEntity> where TEntity : IEntityToDb {

        public static Columns Map() {
            var columns = new Columns();
            var props = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var attribute = prop.GetCustomAttribute<DbColumnAttribute>();
                var columnName = prop.Name;

                if (attribute != null)
                    columnName = attribute.Column;

                    columns.Add(columnName);
            }
            return columns;
        }
    }
}
