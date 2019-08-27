using System;

namespace LicitProd.Entities
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class DbColumnAttribute : Attribute
    {
        public string Column { get; }

        public DbColumnAttribute(string column)
        {
            Column = column;
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class DbTableAttribute : Attribute
    {
        public string TableName { get; }

        public DbTableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}


