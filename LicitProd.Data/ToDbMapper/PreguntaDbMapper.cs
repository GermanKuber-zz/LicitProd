using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class PreguntaDbMapper : ObjectToDbMapper<PreguntaConcurso>
    {
        public PreguntaDbMapper() : base("Preguntas")
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