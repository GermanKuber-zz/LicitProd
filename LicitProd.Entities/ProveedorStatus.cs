using System;

namespace LicitProd.Entities
{
    public class TerminosYCondiciones : Entity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public TerminosYCondiciones(string nombre, string descripcion)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
        }

        public TerminosYCondiciones()
        {
        }
    }
    public enum ProveedorStatus
    {
    }
}