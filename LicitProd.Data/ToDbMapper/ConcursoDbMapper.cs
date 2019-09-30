using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class ConcursoDbMapper : ObjectToDbMapper<Concurso>
    {
        public ConcursoDbMapper() : base("Concursos")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
               .PrimaryKey();
            Set(x => x.IsValid)
                .Ignore();

        }
    }
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


