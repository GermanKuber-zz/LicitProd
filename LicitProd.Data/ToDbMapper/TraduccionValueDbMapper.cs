using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class TraduccionValueDbMapper : ObjectToDbMapper<TraduccionValue>
    {
        public TraduccionValueDbMapper() : base("Traducciones")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
            Set(x => x.IdiomaId)
                .Column("Id_Idioma");
            Set(x => x.TerminoId)
                .Column("Id_Termino");

        }
    }
}