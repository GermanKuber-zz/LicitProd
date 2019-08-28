using System;
using System.Reflection;

namespace LicitProd.Entities
{
    public class DbMapperContainer
    {
        public MemberInfo MemberInfo { get; }
        public string ColumnName { get; private set; }
        public bool HasColumnName { get; private set; }
        public bool IsPrimaryKey { get; private set; }
        public string PropertyName => MemberInfo.Name;

        public DbMapperContainer(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo ?? throw new ArgumentNullException(nameof(memberInfo));
        }
        public DbMapperContainer Column(string colName)
        {
            ColumnName = colName;
            HasColumnName = true;
            return this;
        }
        public DbMapperContainer PrimaryKey()
        {
            IsPrimaryKey = true;
            return this;
        }
    }
}


