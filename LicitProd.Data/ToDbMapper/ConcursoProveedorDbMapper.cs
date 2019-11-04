using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class ConcursoProveedorDbMapper : ObjectToDbMapper<ConcursoProveedorDbMapper.ConcursoProveedor>
    {
        public ConcursoProveedorDbMapper() : base("Concurso_Proveedor")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
        }
        public class ConcursoProveedor : Entity
        {
            public int ProveedorId { get; set; }
            public int ConcursoId { get; set; }
            public int Status { get; set; }
        }
    }
}