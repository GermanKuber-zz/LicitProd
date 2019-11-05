using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;
using System;

namespace LicitProd.Data.ToDbMapper
{


    public class ConcursoParaOfertarDbMapper : ObjectToDbMapper<ConcursoParaOfertar>
    {
        public ConcursoParaOfertarDbMapper() : base("Concursos")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
               .PrimaryKey();
            Set(x => x.TerminosYCondicionesId)
                    .Column("TerminosYCondiciones_Id");

        }
    }
    public class ConcursoParaOfertar : Entity
    {
        public int Status { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaApertura { get; set; }
        public int TerminosYCondicionesId { get; set; }
        public bool AceptoTerminosYCondiciones { get; set; }
    }
}


