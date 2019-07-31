using System;

namespace LicitProd.Entities
{
    public class Log
    {
        public int Id { get; }
        public string Nombre { get; }
        public string Descripcion { get; }
        public LogType Type { get; }
        public DateTime Fecha { get; } = DateTime.Now;

        public Log(string nombre, string descripcion, LogType type)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            Type = type;
        }
    }
}
