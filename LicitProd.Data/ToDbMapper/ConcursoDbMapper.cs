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
    public class VerificableDbMapper : ObjectToDbMapper<Verificable>
    {
        public VerificableDbMapper() : base("Concursos")
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
}


