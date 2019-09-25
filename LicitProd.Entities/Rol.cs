using System.Collections.Generic;

namespace LicitProd.Entities
{
    public class Rol : Permission
    {


        public Rol()
        {

        }
        public Rol(string roleName)
        {
            Nombre = roleName;
        }
        public Rol(int id, string roleName)
        {
            Nombre = roleName;
            Id = id;
        }

        public override void Add(Permission permission)
        {
            Permissions.Add(permission);
        }

        public override void Remove(Permission permission)
        {
            Permissions.Remove(permission);
        }
        public override Response<bool> HasAccess(PermissionsEnum permission)
        {
            foreach (var item in Permissions)
            {
                var result = item.HasAccess(permission);
                if (result.SuccessResult)
                    return Response<bool>.Ok(result.Result);
                else if(Nombre == permission.ToString())
                    return Response<bool>.Ok(true);

            }
            return Response<bool>.Error();
        }
    }
}
