using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Infrastructure.Objects
{
    public interface IObjectToDbMapper<TEntity> where TEntity : new()
    {
        DbMapperContainer Set<TProperty>(Expression<Func<TEntity, TProperty>> dataValueField);
        Response<DbMapperContainer> GetColumnName(string propertyName);
        string TableName { get; }
        Response<string> GetPk();
        IReadOnlyList<string> GetColumns();
        Parameters GetParameters(TEntity entity);
    }
}