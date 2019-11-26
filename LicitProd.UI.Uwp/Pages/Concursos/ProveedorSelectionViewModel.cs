using LicitProd.Entities;

namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public class ProveedorSelectionViewModel
    {
        public Proveedor Proveedor { get; set; }
        public ConcursoProveedor ConcursoProveedor { get; }

        public bool Selected { get; set; }
        public ProveedorSelectionViewModel(Proveedor proveedor, ConcursoProveedor concursoProveedor)
        {
            Proveedor = proveedor;
            ConcursoProveedor = concursoProveedor;

        }
        public ProveedorSelectionViewModel(Proveedor proveedor)
        {
            Proveedor = proveedor;

        }
    }
}