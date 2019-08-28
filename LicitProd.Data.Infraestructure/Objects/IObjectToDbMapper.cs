using LicitProd.Services;
using System;
using System.Linq.Expressions;

namespace LicitProd.Entities
{
    public interface IObjectToDbMapper<TEntity> where TEntity : new()
    {
        DbMapperContainer Set<TProperty>(Expression<Func<TEntity, TProperty>> dataValueField);
        Response<string> GetColumnName(string propertyName);
        string TableName { get; }
    }
}