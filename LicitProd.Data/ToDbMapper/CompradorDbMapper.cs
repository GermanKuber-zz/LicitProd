using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class CompradorDbMapper : ObjectToDbMapper<Comprador>
    {
        public CompradorDbMapper() : base("Compradores")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
            Set(x => x.Usuario)
                .Ignore();
        }
    }
}