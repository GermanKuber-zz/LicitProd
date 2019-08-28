using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LicitProd.Services;

namespace LicitProd.Entities
{
    public abstract class ObjectToDbMapper<TEntity> : IObjectToDbMapper<TEntity> where TEntity : new()
    {
        public string TableName { get; private set; }
        private readonly TEntity _entity;
        public List<DbMapperContainer> _dbMapperContainer = new List<DbMapperContainer>();
        public ObjectToDbMapper(string tableName)
        {
            _entity = new TEntity();
            Map();
            TableName = tableName;
        }
        public ObjectToDbMapper()
        {

        }
        public DbMapperContainer Set<TProperty>(Expression<Func<TEntity, TProperty>> dataValueField)
        {
            var property = getMemberInfo(dataValueField);
            var container = new DbMapperContainer(property);
            _dbMapperContainer.Add(container);
            return container;
        }
        protected virtual void Map() { }
        private MemberInfo getMemberInfo<TObject, TProperty>(Expression<Func<TObject, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
            {
                return member.Member;
            }
            throw new ArgumentException("Member does not exist.");
        }

        public Response<string> GetColumnName(string propertyName)
        {
            var result = _dbMapperContainer.Where(x => x.PropertyName == propertyName).FirstOrDefault();
            if (result != null)
                return Response<string>.Ok(result.ColumnName);

            return Response<string>.Error();
        }
    }

}


