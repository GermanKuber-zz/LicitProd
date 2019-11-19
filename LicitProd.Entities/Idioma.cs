using System;
using System.Collections.Generic;

namespace LicitProd.Entities
{
    public class PreguntaConcurso : Entity
    {
        public string Pregunta { get; set; }
        public string Respuesta { get; set; } = String.Empty;
        public int ConcursoProveedorId { get; set; }
    }

    public class Idioma : Entity
    {
        public string Nombre { get; set; }
        public List<TraduccionValue> Traducciones { get; protected set; }

        public Idioma(string nombre, List<TraduccionValue> traducciones)
        {
            Nombre = nombre;
            Traducciones = traducciones;
        }

        public Idioma()
        {
            
        }
        public void SetTraducciones(List<TraduccionValue> traducciones) => Traducciones = traducciones;
    }
}