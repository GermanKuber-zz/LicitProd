using System.Collections.Generic;
using System.Data;
using LicitProd.Entities;

namespace LicitProd.Data.Infrastructure.Objects
{
    public interface IDbToObjectMapper<TEntity> where TEntity : IEntityToDb
    {
        List<TEntity> MapList(DataTable dataTable);
        TEntity Map(DataTable dataTable);
    }
}
