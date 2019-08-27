using LicitProd.Entities;
using LicitProd.Infraestructure;
using LicitProd.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LicitProd.Data
{
    public class BaseRepository<TEntity> where TEntity : IEntityToDb
    {
        protected readonly SqlAccessService SqlAccessService;

        public BaseRepository()
        {
            SqlAccessService = new SqlAccessService();

        }
        public List<TEntity> Get()
        {
            var dc = new Default();
            var loadableTypes = ReflectionHelper.GetClassesImplementingAnInterface(dc.GetType().Assembly, typeof(DbMapper<TEntity>)).Item2;

            var type = loadableTypes.FirstOrDefault(x=> x.Name.Contains(typeof(TEntity).Name));

            if (type == null)
                throw new Exception("El mapper requerido no existe : " + type.ToString());
            var mapper = (DbMapper<TEntity>)Activator.CreateInstance(type);

            var dbTableAttribute = typeof(TEntity).GetCustomAttribute<DbTableAttribute>(false);
            if (dbTableAttribute == null)
                throw new Exception($"La clase {typeof(TEntity).Name} no tiene un atributo DbTableAttribute.");

            return mapper.MapList(SqlAccessService.SelectData(dbTableAttribute.TableName,
                 EntityToColumns<TEntity>.Map()
                .Send()));
        }
    
    }
}
