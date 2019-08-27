using System;

namespace LicitProd.Entities
{
    public class Log
    {
        public int Id { get; protected set; }
        public string Nombre { get; protected set; }
        public string Descripcion { get; protected set; }
        public LogType Type { get; protected set; }
        public DateTime Fecha { get; protected set; } = DateTime.Now;
        public Log()
        {

        }
        public Log(string nombre, string descripcion, LogType type)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            Type = type;
        }
    }
}
