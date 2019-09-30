using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class BackupDbMapper : ObjectToDbMapper<Backup>
    {
        public BackupDbMapper() : base("Backups")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
        }
    }
}