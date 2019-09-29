using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class VerificableDbMapper : ObjectToDbMapper<ConcursoVerificable>
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