using LicitProd.Services;
using System.Collections.Generic;

namespace LicitProd.Entities
{
    public class Rol : Permission
    {
        public List<Permission> Permissions = new List<Permission>();


        public Rol()
        {

        }
        public Rol(PermissionsEnum roleName)
        {
            Name = roleName;
        }
        public Rol(int id, PermissionsEnum roleName)
        {
            Name = roleName;
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
                else if(Name == permission)
                    return Response<bool>.Ok(true);

            }
            return Response<bool>.Error();
        }
    }
}
