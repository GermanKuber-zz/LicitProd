namespace LicitProd.Entities
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
        }
    }
}


