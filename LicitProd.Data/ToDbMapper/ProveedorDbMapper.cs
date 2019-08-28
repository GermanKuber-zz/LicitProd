namespace LicitProd.Entities
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


