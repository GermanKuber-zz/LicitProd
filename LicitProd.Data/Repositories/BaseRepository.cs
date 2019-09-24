using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;
using LicitProd.Infrastructure;

namespace LicitProd.Data.Repositories
{
    public class BaseRepository<TEntity> where TEntity : IEntityToDb, new()
    {
        protected readonly SqlAccessService<TEntity> SqlAccessService = new SqlAccessService<TEntity>();

        protected async Task<Response<List<TEntity>>> GetAsync() =>
            ReturnResult((await CreateMapper()).MapList((await SqlAccessService.SelectData((await EntityToColumns<TEntity>.MapAsync())
                 .Send()))));

        protected async Task<Response<List<TEntity>>> GetAsync(List<Parameter> parameters) =>
            ReturnResult((await CreateMapper()).MapList((await SqlAccessService.SelectData(parameters, (await EntityToColumns<TEntity>.MapAsync())
                 .Send()))));

        public async Task<Response<TEntity>> GetByIdAsync(int id) =>
                    (await ObjectToDbMapperFactory<TEntity>.Create()).GetPk()
                             .Success(async x =>
                             Response<TEntity>.From((await CreateMapper()).Map(await SqlAccessService.SelectData(new Parameters()
                                     .Add(x, id)
                                     .Send(),
                                     (await EntityToColumns<TEntity>.MapAsync()).Send()))))
                             .Error(x => Response<TEntity>.Error());

        public async Task<int> InsertDataAsync(TEntity entity) =>
            await SqlAccessService.InsertDataAsync(entity);
        protected static Response<List<TEntity>> ReturnResult(List<TEntity> result) =>
            Response<TEntity>.From(result);
        protected static Response<TEntity> ReturnResult(TEntity result) =>
           Response<TEntity>.From(result);

        protected async Task<IDbToObjectMapper<TEntity>> CreateMapper()
        {
            return await Task.Run(() =>
            {
                var loadableTypes = ReflectionHelper.GetClassesImplementingAnInterface<IDbToObjectMapper<TEntity>>();


                var type = loadableTypes.FirstOrDefault(x => x.Name.Contains(typeof(TEntity).Name));

                if (type == null)
                {
                    type = loadableTypes.FirstOrDefault(x => x.Name.Contains("All"));
                    if (type == null)
                        throw new Exception("El mapper requerido no existe : " + type.ToString());
                    Type[] typeArgs = { typeof(TEntity) };
                    return (IDbToObjectMapper<TEntity>)Activator.CreateInstance(type.MakeGenericType(typeArgs));
                }
                return (IDbToObjectMapper<TEntity>)Activator.CreateInstance(type);
            });
        }
    }

}
