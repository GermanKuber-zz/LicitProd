namespace LicitProd.Entities
{
    public class SinglePermission : Permission
    {

        public SinglePermission(PermissionsEnum permissionName)
        {
            Nombre = permissionName.ToString();
        }

        public SinglePermission(int id, PermissionsEnum permissionName)
        {
            Nombre = permissionName.ToString();
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
