using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicitProd.Entities
{
    public enum LogType
    {
        Informacion
    }
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
    public class Usuario
    {
        public int Id { get; }
        public string Email { get; }
        public string HashPassword { get; }

        public Usuario(int id, string email, string hashPassword)
        {
            Id = id;
            Email = email ?? throw new ArgumentNullException(nameof(email));
            HashPassword = hashPassword ?? throw new ArgumentNullException(nameof(hashPassword));
        }

        public Usuario(string email, string hashPassword)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            HashPassword = hashPassword ?? throw new ArgumentNullException(nameof(hashPassword));
        }
    }
}
