using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class ConfiguracionDbMapper : ObjectToDbMapper<Configuracion>
    {
        public ConfiguracionDbMapper() : base("Configuraciones")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
            Set(x => x.IdiomaId)
                .Column("Idioma_Id");
            Set(x => x.UsuarioId)
                .Column("Usuario_Id");
            
        }
    }
}