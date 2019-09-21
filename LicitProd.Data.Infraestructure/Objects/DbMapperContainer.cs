using System;
using System.Data;
using System.Reflection;

namespace LicitProd.Entities
{
    public class DbMapperContainer
    {
        public MemberInfo MemberInfo { get; }
        public string ColumnName { get; private set; }
        public bool HasColumnName { get; private set; }
        public bool IsPrimaryKey { get; private set; }
        public bool IsIgnore { get; private set; }
        public SqlDbType SqlDbType { get; private set; }
        public string PropertyName => MemberInfo.Name;
        public bool initialized { get; private set; } = false;

        public DbMapperContainer(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo ?? throw new ArgumentNullException(nameof(memberInfo));
        }
        public DbMapperContainer Column(string colName)
        {
            initialized = true;
            ColumnName = colName;
            HasColumnName = true;
            return this;
        }
        public DbMapperContainer Type(SqlDbType sqlDbType)
        {
            initialized = true;

            SqlDbType = sqlDbType;
            return this;
        }
        public DbMapperContainer PrimaryKey()
        {
            initialized = true;

            IsPrimaryKey = true;
            return this;
        }
        public DbMapperContainer Ignore()
        {
            initialized = true;

            IsIgnore = true;
            return this;
        }
    }
}


