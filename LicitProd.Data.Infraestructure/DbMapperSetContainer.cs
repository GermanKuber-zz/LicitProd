using System;

namespace LicitProd.Entities
{
    public class DbMapperSetContainer
    {
        public string ColumnName { get; }

        public DbMapperSetContainer(string columnName)
        {
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
        }
    }

}


