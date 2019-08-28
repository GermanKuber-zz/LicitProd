using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace LicitProd.Entities
{
    public abstract class DbMapper2<TEntity> where TEntity : new()
    {
        private Dictionary<string, DbMapperSetContainer> _toMap = new Dictionary<string, DbMapperSetContainer>();
        private TEntity _entity;

        public DbMapper2()
        {
            _entity = new TEntity();
            Map();
        }
        public void Set<TProperty>(Expression<Func<TEntity, TProperty>> dataValueField, DbMapperSetContainer dbMapperSetContainer)
        {
            var property = getMemberInfo(dataValueField);
            _toMap.Add(property.Name, dbMapperSetContainer);
        }
        protected abstract void Map();
        private MemberInfo getMemberInfo<TObject, TProperty>(Expression<Func<TObject, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
            {
                return member.Member;
            }
            throw new ArgumentException("Member does not exist.");
        }
    }

}


