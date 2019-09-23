namespace LicitProd.Entities
{
    public abstract class Permission : Entity
    {
        public PermissionsEnum Name { get; set; }
        public abstract void Add(Permission permission);
        public abstract void Remove(Permission permission);

        public Permission()
        {

        }
        public virtual Response<bool> HasAccess(PermissionsEnum permiso) =>
            Response<bool>.From(() => Name == permiso, true);
    }
}
