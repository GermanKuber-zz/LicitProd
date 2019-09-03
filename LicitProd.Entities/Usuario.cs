using System;

namespace LicitProd.Entities
{
    public class Usuario : Entity
    {
        public string Email { get; protected set; }
        public string HashPassword { get; protected set; }
        public Rol Rol { get; protected set; }

        public Usuario()
        {

        }
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
        public void SetRol(Rol rol) => Rol = rol;
    }

}


