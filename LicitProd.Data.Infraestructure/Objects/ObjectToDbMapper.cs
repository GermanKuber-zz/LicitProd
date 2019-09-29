using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;
using LicitProd.Infrastructure;

namespace LicitProd.Data.Infrastructure.Objects
{
    public abstract class ObjectToDbMapper<TEntity> : IObjectToDbMapper<TEntity> where TEntity : new()
    {
        public string TableName { get; private set; }
        private readonly TEntity _entity;
        public List<DbMapperContainer> DbMapperContainer = new List<DbMapperContainer>();
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
            var property = GetMemberInfo(dataValueField);
            var container = new DbMapperContainer(property);
            DbMapperContainer.Add(container);
            return container;
        }
        protected virtual void Map() { }
        private MemberInfo GetMemberInfo<TObject, TProperty>(Expression<Func<TObject, TProperty>> expression)
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
            var result = DbMapperContainer.Where(x => x.PropertyName == propertyName).FirstOrDefault();
            if (result != null && result.Initialized)
                return Response<DbMapperContainer>.Ok(result);

            return Response<DbMapperContainer>.Error();
        }
        public Response<string> GetPk()
        {
            var pk = DbMapperContainer.FirstOrDefault(x => x.IsPrimaryKey);
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
                                if (prop.PropertyType.IsEnum)
                                    enumToAdd = Enum.GetUnderlyingType(prop.PropertyType);
                                if (!string.IsNullOrWhiteSpace(x.ColumnName))
                                    columnName = x.ColumnName;

                                if (enumToAdd != null)
                                    parameters.Add(columnName, typse.ToString(), x.SqlDbType);
                                else if (!x.IsPrimaryKey && !x.IsIgnore)
                                    parameters.Add(columnName, (dynamic)typse, x.SqlDbType);

                            })
                            .Error(e =>
                            {
                                Type enumToAdd = default;
                                if (prop.PropertyType.IsEnum)
                                    enumToAdd = Enum.GetUnderlyingType(prop.PropertyType);


                                if (enumToAdd != null)
                                    parameters.Add(prop.Name, typse.ToString(), SqlDbType.NVarChar);
                                else parameters.Add(prop.Name, (dynamic)typse);
                            });

                });
            return parameters;
        }
    }

}


