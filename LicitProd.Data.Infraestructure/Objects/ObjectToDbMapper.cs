using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LicitProd.Data;
using LicitProd.Infraestructure;
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

        public Response<DbMapperContainer> GetColumnName(string propertyName)
        {
            var result = _dbMapperContainer.Where(x => x.PropertyName == propertyName).FirstOrDefault();
            if (result != null && result.initialized)
                return Response<DbMapperContainer>.Ok(result);

            return Response<DbMapperContainer>.Error();
        }
        public Response<string> GetPk()
        {
            var pk = _dbMapperContainer.FirstOrDefault(x => x.IsPrimaryKey);
            if (pk == null)
                return Response<string>.Error();
            return Response<string>.Ok(string.IsNullOrWhiteSpace(pk.ColumnName) ? pk.PropertyName : pk.ColumnName);
        }

        public IReadOnlyList<string> GetColumns()
        {
            return ReflectionHelper.GetListOfProperties<TEntity>()
                 .Select(prop =>
                 {
                     var columnName = prop.Name;
                     GetColumnName(prop.Name)
                         .Success(x => columnName = x.ColumnName);
                     return columnName;
                 }).ToList().AsReadOnly();
        }

        public Parameters GetParameters(TEntity entity)
        {
            var parameters = new Parameters();
            ReflectionHelper.GetListOfProperties<TEntity>()
                .ToList()
                .ForEach(prop =>
                {
                    var value = prop.GetValue(entity, null);
                    var type = prop.GetType();
                    var columnName = prop.Name;
                    var typse = Convert.ChangeType(value, prop.PropertyType);

                    GetColumnName(prop.Name)
                            .Success(x =>
                            {
                                Type enumToAdd = default;
                                try
                                {
                                    enumToAdd = Enum.GetUnderlyingType(prop.PropertyType);
                                }
                                catch (Exception)
                                {
                                }

                                if (enumToAdd != null)
                                    parameters.Add(prop.Name, typse.ToString(), x.SqlDbType);
                                else if (!x.IsPrimaryKey && !x.IsIgnore)
                                    parameters.Add(prop.Name, (dynamic)typse, x.SqlDbType);

                            })
                            .Error(e =>
                            {
                                parameters.Add(prop.Name, (dynamic)typse);
                            });
                });
            return parameters;
        }
    }

}


