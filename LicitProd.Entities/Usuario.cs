using System;

namespace LicitProd.Entities
{
    [DbTableAttribute("Usuarios")]
    public class Usuario: IEntityToDb
    {
        public int Id { get; protected set; }
        public string Email { get; protected set; }
        [DbColumnAttribute("Password")]
        public string HashPassword { get; protected set; }

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
    }
  
}


