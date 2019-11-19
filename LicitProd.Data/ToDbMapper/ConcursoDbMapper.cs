using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{


    public class ConcursoDbMapper : ObjectToDbMapper<Concurso>
    {
        public ConcursoDbMapper() : base("Concursos")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
               .PrimaryKey();
            Set(x => x.IsValid)
                .Ignore();
            Set(x => x.Comprador)
                .Ignore();
            Set(x => x.Status)
             .Column("Status");
            Set(x => x.ConcursoProveedores)
                .Ignore();
            Set(x => x.TerminosYCondicionesId)
                    .Column("TerminosYCondiciones_Id");
            Set(x => x.CompradorId)
                .Column("Comprador_Id");
            Set(x => x.CentroOperativoId)
                .Column("CentroOperativo_Id");

        }
        
    }
}


