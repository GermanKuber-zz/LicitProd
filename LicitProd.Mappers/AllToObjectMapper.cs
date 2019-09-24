using LicitProd.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LicitProd.Data.Infrastructure.Extensions;
using LicitProd.Data.Infrastructure.Objects;
using System.Threading.Tasks;
using System;

namespace LicitProd.Mappers
{
    public class AllToObjectMapper<TEntity> : IDbToObjectMapper<TEntity> where TEntity : IEntityToDb, new()
    {
        public TEntity Map(DataTable dataTable) =>
               AsyncHelper.CallAsyncMethod(()=> MapHelper<TEntity>.FillObjectAsync(dataTable.Rows[0]));

        public List<TEntity> MapList(DataTable dataTable) =>
               dataTable.Rows.ListOfRows().Select(row => AsyncHelper.CallAsyncMethod(() => MapHelper<TEntity>.FillObjectAsync(row)))
                .ToList();
    }

    public static class AsyncHelper
    {

        public static TReturn CallAsyncMethod<TReturn>(Func<Task<TReturn>> callback)
        {
            var returnValue = default(TReturn);
            Task.Run(async () =>
            {
                returnValue = await callback();
            }).Wait();
            return returnValue;
        }
        public static void CallAsyncMethodVoid(Func<Task> callback)
        {
            Task.Run(async () =>
            {
                await callback();
            }).Wait();
        }
    }
}
