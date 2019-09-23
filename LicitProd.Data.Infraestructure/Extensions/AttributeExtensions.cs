using System;
using System.Reflection;
using LicitProd.Entities;

namespace LicitProd.Data.Infrastructure.Extensions
{
    public static class AttributeExtensions<TAttribute, TEntity> where TEntity : IEntityToDb
                                                       where TAttribute : Attribute
    {
        public static TAttribute GetAttribute()
        {
            var dbTableAttribute = typeof(TEntity).GetCustomAttribute<TAttribute>(false);
            if (dbTableAttribute == null)
                throw new Exception($"La clase {typeof(TEntity).Name} no tiene un atributo DbTableAttribute.");
            return dbTableAttribute;
        }
    }

}
