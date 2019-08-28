using System;
using System.Linq.Expressions;

namespace LicitProd.Entities
{
    public interface IDbMapper2<TEntity> where TEntity : new()
    {
        DbMapperContainer Set<TProperty>(Expression<Func<TEntity, TProperty>> dataValueField);
    }
}