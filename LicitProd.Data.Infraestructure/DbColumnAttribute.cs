using System;

namespace LicitProd.Data.Infrastructure
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
}


