using System;

namespace LicitProd.Data.Infrastructure
{
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