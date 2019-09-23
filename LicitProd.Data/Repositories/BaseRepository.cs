using System;
using System.Collections.Generic;
using System.Linq;
using LicitProd.Entities;
using LicitProd.Infraestructure;
using LicitProd.Mappers;

namespace LicitProd.Data.Repositories
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

        public Response<TEntity> GetById(int id) =>
                    ObjectToDbMapperFactory<TEntity>.Create().GetPk()
                             .Success(x =>
                             Response<TEntity>.From(CreateMapper().Map(SqlAccessService.SelectData(new Parameters()
                                     .Add(x, id)
                                     .Send(),
                                     EntityToColumns<TEntity>.Map().Send()))))
                             .Error(x => Response<TEntity>.Error());

        protected static Response<List<TEntity>> ReturnResult(List<TEntity> result) =>
            Response<TEntity>.From(result);
        protected static Response<TEntity> ReturnResult(TEntity result) =>
           Response<TEntity>.From(result);

        protected DbToObjectMapper<TEntity> CreateMapper()
        {

            var loadableTypes = ReflectionHelper.GetClassesImplementingAnInterface<DbToObjectMapper<TEntity>>();


            var type = loadableTypes.FirstOrDefault(x => x.Name.Contains(typeof(TEntity).Name));

            if (type == null)
            {
                type = loadableTypes.FirstOrDefault(x => x.Name.Contains("All"));
                if (type == null)
                    throw new Exception("El mapper requerido no existe : " + type.ToString());
                Type[] typeArgs = { typeof(TEntity) };
                return (DbToObjectMapper<TEntity>)Activator.CreateInstance(type.MakeGenericType(typeArgs));
            }
            return (DbToObjectMapper<TEntity>)Activator.CreateInstance(type);
        }
    }

}
