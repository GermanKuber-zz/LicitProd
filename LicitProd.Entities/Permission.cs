using System.Collections.Generic;

namespace LicitProd.Entities
{
    public abstract class Permission : Entity
    {
        public string Nombre { get; set; }
        public abstract void Add(Permission permission);
        public abstract void Remove(Permission permission);
        public List<Permission> Permissions = new List<Permission>();

        public Permission()
        {

        }

        public virtual Response<bool> HasAccess(PermissionsEnum permiso) =>
            HasAccess(permiso.ToString());
        public virtual Response<bool> HasAccess(string permiso) =>
            Response<bool>.From(() => Nombre == permiso.ToString(), true);
    }
}
