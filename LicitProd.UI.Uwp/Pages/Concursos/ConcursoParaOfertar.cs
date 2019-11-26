using LicitProd.Entities;

namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public class ConcursoParaOfertar
    {
        public Concurso Concurso { get; set; }
        public ConcursoProveedor ConcursoProveedor { get; set; }

        public bool Oferte => ConcursoProveedor?.Oferta != null;
        public ConcursoParaOfertar(Concurso concurso, ConcursoProveedor concursoProveedor)
        {
            Concurso = concurso;
            ConcursoProveedor = concursoProveedor;
        }
    }
}