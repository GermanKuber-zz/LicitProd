using System;
using System.Linq.Expressions;

namespace LicitProd.Entities
{
    public interface IObjectToDbMapper<TEntity> where TEntity : new()
    {
        DbMapperContainer Set<TProperty>(Expression<Func<TEntity, TProperty>> dataValueField);
        string GetColumnName(string propertyName);
        bool ExistPropertySettings(string propertyName);
    }
}