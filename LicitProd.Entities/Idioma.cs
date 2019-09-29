using System.Collections.Generic;

namespace LicitProd.Entities
{
    public class Idioma : Entity
    {
        public string Nombre { get; set; }
        public List<TraduccionValue> Traducciones { get; protected set; }

        public void SetTraducciones(List<TraduccionValue> traducciones) => Traducciones = traducciones;
    }
}