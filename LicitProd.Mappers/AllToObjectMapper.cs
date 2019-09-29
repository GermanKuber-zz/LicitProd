using LicitProd.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LicitProd.Data.Infrastructure.Extensions;
using LicitProd.Data.Infrastructure.Objects;

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
}
