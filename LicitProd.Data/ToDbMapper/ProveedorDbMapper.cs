using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class ProveedorDbMapper : ObjectToDbMapper<Proveedor>
    {
        public ProveedorDbMapper() : base("Proveedores")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
               .PrimaryKey();
        }
    }
}


