using LicitProd.Entities;
using LicitProd.Infraestructure;
using System;
using System.Linq;
using System.Reflection;

namespace LicitProd.Data
{
    public static class EntityToColumns<TEntity> where TEntity : IEntityToDb, new()
    {

        
        public static Columns Map() {

            var loadableTypes = ReflectionHelper.GetClassesImplementingAnInterface(new LogDbMapper().GetType().Assembly, typeof(IObjectToDbMapper<TEntity>)).Item2;
            var type = loadableTypes.FirstOrDefault(x => x.Name.Contains(typeof(TEntity).Name));

            if (type == null)
                throw new Exception("El DbMapper requerido no existe : " + type.ToString());

            var objectToDbMapper = (IObjectToDbMapper<TEntity>)Activator.CreateInstance(type);
            var columns = new Columns();
            var props = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var columnName = prop.Name;

                if (objectToDbMapper.ExistPropertySettings(prop.Name))
                    columnName = objectToDbMapper.GetColumnName(prop.Name);

                    columns.Add(columnName);
            }
            return columns;
        }
    }
}
