using LicitProd.Entities;
using LicitProd.Mappers;
using LicitProd.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LicitProd.Data
{
    public class BaseRepository<TEntity> where TEntity : IEntityToDb, new()
    {
        protected readonly SqlAccessService<TEntity> SqlAccessService = new SqlAccessService<TEntity>();

        protected Response<List<TEntity>> Get() =>
            ReturnResult(CreateMapper().MapList(SqlAccessService.SelectData(EntityToColumns<TEntity>.Map()
                 .Send())));
        protected Response<List<TEntity>> Get(List<Parameter> parameters) =>
            ReturnResult(CreateMapper().MapList(SqlAccessService.SelectData(parameters, EntityToColumns<TEntity>.Map()
                 .Send())));

        private static Response<List<TEntity>> ReturnResult(List<TEntity> result)
        {
            if (!result.Any())
                return Response<List<TEntity>>.Error();

            return Response<List<TEntity>>.Ok(result);
        }

        private DbToObjectMapper<TEntity> CreateMapper()
        {
            var dc = new Default();
            var loadableTypes = ReflectionHelper.GetClassesImplementingAnInterface(dc.GetType().Assembly, typeof(DbToObjectMapper<TEntity>)).Item2;

            var type = loadableTypes.FirstOrDefault(x => x.Name.Contains(typeof(TEntity).Name));

            if (type == null)
                throw new Exception("El mapper requerido no existe : " + type.ToString());
            return (DbToObjectMapper<TEntity>)Activator.CreateInstance(type);
        }
    }

}
