﻿using LicitProd.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LicitProd.Mappers
{
    public class Default { }
    public interface DbMapper< TEntity> where TEntity : IEntityToDb
    {
        List<TEntity> MapList(DataTable dataTable);
        TEntity Map(DataTable dataTable);
    }
    public  class LogMapper : DbMapper<Log>
    {
        public Log Map(DataTable dataTable)
        {
            throw new System.NotImplementedException();
        }

        public  List<Log> MapList(DataTable dataTable) =>
            dataTable.Rows.ListOfRows().Select(row => MapHelper.FillObject<Log>(row))
                .ToList();

    }

  

}
