using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LicitProd.Entities
{
    public abstract class ObjectToDbMapper<TEntity> : IObjectToDbMapper<TEntity> where TEntity : new()
    {
        public string TableName { get; private set; }
        private Dictionary<string, DbMapperSetContainer> _toMap = new Dictionary<string, DbMapperSetContainer>();
        private TEntity _entity;
        public List<DbMapperContainer> _dbMapperContainer = new List<DbMapperContainer>();
        public ObjectToDbMapper()
        {
            _entity = new TEntity();
            Map();
        }
        public void SetTableName(string tableName)
        {
            TableName = tableName;
        }
        public DbMapperContainer Set<TProperty>(Expression<Func<TEntity, TProperty>> dataValueField)
        {
            var property = getMemberInfo(dataValueField);
            //_toMap.Add(property.Name, dbMapperSetContainer);
            var container = new DbMapperContainer(property);
            _dbMapperContainer.Add(container);
            return container;
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

        public string GetColumnName(string propertyName)
        {
            var result = _dbMapperContainer.Where(x => x.PropertyName == propertyName).FirstOrDefault();
            if (result != null)
                return result.ColumnName;
            return string.Empty;
        }
        public bool ExistPropertySettings(string propertyName) => _dbMapperContainer.Any(x => x.PropertyName == propertyName);
    }

}


