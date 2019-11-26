using LicitProd.Entities;

namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public class OfertaViewModel
    {
        public ConcursoProveedor ConcursoProveedor { get; }
        public bool Selected
        {
            get { return ConcursoProveedor.Ganador; }
            set { }
        }
        public OfertaViewModel(ConcursoProveedor concursoProveedor)
        {
            ConcursoProveedor = concursoProveedor;
        }
    }
}