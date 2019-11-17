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
            Set(x => x.ConcursoProveedorId)
                .Column("Concurso_Proveedor_Id");
        }
    }
}