namespace LicitProd.Entities
{
    public class SinglePermission : Permission
    {

        public SinglePermission(PermissionsEnum permissionName)
        {
            Name = permissionName;
        }

        public SinglePermission(int id, PermissionsEnum permissionName)
        {
            Name = permissionName;
            Id = id;
        }


        public override void Add(Permission permission)
        {
        }

        public override void Remove(Permission permission)
        {
        }

    }
}
