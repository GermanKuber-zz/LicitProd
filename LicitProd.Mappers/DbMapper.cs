using LicitProd.Entities;
using System.Collections.Generic;
using System.Data;

namespace LicitProd.Mappers
{
    public interface DbMapper< TEntity> where TEntity : IEntityToDb
    {
        List<TEntity> MapList(DataTable dataTable);
        TEntity Map(DataTable dataTable);
    }

  

}
