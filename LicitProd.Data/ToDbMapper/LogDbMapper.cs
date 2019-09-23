using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class LogDbMapper : ObjectToDbMapper<Log>
    {
        public LogDbMapper() : base("Logs")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
               .PrimaryKey();
            Set(x => x.Type)
               .Column("Type")
               .Type(System.Data.SqlDbType.NVarChar);
        }
    }
}


