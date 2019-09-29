using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class IdiomaDbMapper : ObjectToDbMapper<Idioma>
    {
        public IdiomaDbMapper() : base("Idiomas")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
            Set(x => x.Traducciones)
                .Ignore();

        }
    }
}