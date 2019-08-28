using System;
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

}


