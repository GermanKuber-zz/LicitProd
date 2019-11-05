using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class OfertaDbMapper : ObjectToDbMapper<Oferta>
    {
        public OfertaDbMapper() : base("Ofertas")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
            Set(x => x.Concurso)
                .Ignore();
            Set(x => x.Proveedor)
                .Ignore();
            Set(x => x.ConcursoId)
                .Column("Concurso_Id");
            Set(x => x.ProveedorId)
                .Column("Proveedor_Id");
        }
    }
}