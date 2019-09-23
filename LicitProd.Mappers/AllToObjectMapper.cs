using LicitProd.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LicitProd.Mappers
{
    public class AllToObjectMapper<TEntity> : DbToObjectMapper<TEntity> where TEntity : IEntityToDb, new()
    {
        public TEntity Map(DataTable dataTable) =>
               MapHelper<TEntity>.FillObject(dataTable.Rows[0]);

        public List<TEntity> MapList(DataTable dataTable) =>
               dataTable.Rows.ListOfRows().Select(row => MapHelper<TEntity>.FillObject(row))
                .ToList();
    }
}
