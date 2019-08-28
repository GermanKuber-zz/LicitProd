using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace LicitProd.Entities
{
    public class DbMapperContainer {
        public MemberInfo MemberInfo { get;  }
        public string _columnName { get; private set; }

        public DbMapperContainer(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo ?? throw new ArgumentNullException(nameof(memberInfo));
        }
        public DbMapperContainer Column(string colName) {
            _columnName = colName;
            return this;
        }
    }
    public class EntityDefault { }
    public abstract class DbMapper2<TEntity> : IDbMapper2<TEntity> where TEntity : new()
    {
        private Dictionary<string, DbMapperSetContainer> _toMap = new Dictionary<string, DbMapperSetContainer>();
        private TEntity _entity;
        public List<DbMapperContainer> _dbMapperContainer = new List<DbMapperContainer>();
        public DbMapper2()
        {
            _entity = new TEntity();
            Map();
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
    }

}


