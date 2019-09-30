using System;

namespace LicitProd.Entities
{
    public class Usuario : Entity
    {
        public string Email { get; protected set; }
        public string HashPassword { get; protected set; }
        public Rol Rol { get; protected set; }

        private int _rolId;
        public int RolId
        {
            get => Rol.Id;
            protected set => _rolId = value;
        }


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
        public Usuario(string email, string hashPassword, Rol rol)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            HashPassword = hashPassword ?? throw new ArgumentNullException(nameof(hashPassword));
            Rol = rol;
        }

        public void ChangePassword(string hashPassword)
        {
            HashPassword = hashPassword;
        }
        public void SetRol(Rol rol) => Rol = rol;
    }

}


