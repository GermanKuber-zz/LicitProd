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

        public async Task<Response<List<TEntity>>> GetAsync() =>
            ReturnResult((await CreateMapper()).MapList((await SqlAccessService.SelectData((await EntityToColumns<TEntity>.MapAsync())
                 .Send()))));

        public async Task<Response<List<TEntity>>> GetAsync(List<Parameter> parameters, List<string> selectColumns) =>
            ReturnResult((await CreateMapper()).MapList((await SqlAccessService.SelectData(parameters, selectColumns))));
        public async Task<Response<List<TEntity>>> GetAsync(List<Parameter> parameters) =>
            ReturnResult((await CreateMapper()).MapList((await SqlAccessService.SelectData(parameters, (await EntityToColumns<TEntity>.MapAsync())
                .Send()))));

        public async Task<Response<List<TEntity>>> GetAsync(Parameters parameters) => await GetAsync(parameters.Send());

        public async Task<Response<TEntity>> GetByIdAsync(int id) =>
            (await ObjectToDbMapperFactory<TEntity>.Create()).GetPk()
            .Success(async x =>
                Response<TEntity>.From((await CreateMapper()).Map(await SqlAccessService.SelectData(new Parameters()
                        .Add(x, id)
                        .Send(),
                    (await EntityToColumns<TEntity>.MapAsync()).Send()))))
            .Error(x => Response<TEntity>.Error());

        private Parameters GetParameters(List<int> ids)
        {
            return ids.Aggregate(new Parameters(), (acc, x) => acc.Add("Id", x));
        }
        public async Task<List<TEntity>> GetByIdsAsync(List<int> ids) =>
            (await ObjectToDbMapperFactory<TEntity>.Create()).GetPk()
            .Success(async x =>
                (await CreateMapper()).MapList(await SqlAccessService.SelectDataIn(new Parameters()
                        .Add("Id", string.Empty),
                    GetParameters(ids), new List<string>())));

        public async Task<Response<TEntity>> InsertDataAsync(TEntity entity)
        {
            await SqlAccessService.InsertDataAsync(entity);
            return Response<TEntity>.Ok(entity);
        }
        public async Task<Response<TEntity>> UpdateDataAsync(TEntity entity)
        {
            await SqlAccessService.UpdateDataAsync(entity);
            return Response<TEntity>.Ok(entity);
        }

       public async Task DeleteAsync(Parameters where)
        {
            await SqlAccessService.DeleteAsync(where);
        }


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
