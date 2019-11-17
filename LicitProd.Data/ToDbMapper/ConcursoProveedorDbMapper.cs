using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class ConcursoProveedorDbMapper : ObjectToDbMapper<ConcursoProveedor>
    {
        public ConcursoProveedorDbMapper() : base("Concurso_Proveedor")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
            Set(x => x.Proveedor)
                .Ignore();
            Set(x => x.Oferta)
                .Ignore();
        }
        
    }
}