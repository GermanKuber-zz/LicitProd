using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class BackupDbMapper : ObjectToDbMapper<HitoConcurso>
    {
        public BackupDbMapper() : base("HitoConcurso")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
        }
    }
}