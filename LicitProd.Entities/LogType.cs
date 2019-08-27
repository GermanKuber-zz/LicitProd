using System;
using System.Collections.Generic;

namespace LicitProd.Entities
{
    public enum PermissionsEnum {
        ReadLogs,
        DeleteLogs,
        ReadConcurso,
        DeleteConcurso,
        EditConcurso,
        ReadProveedor,
        DeleteProveedor,
        EditProveedor
    }

    public abstract class Permission
    {
        public string Name { get; set; }
        public abstract void Add(Permission permission);
        public abstract void Remove(Permission permission);
        public virtual bool HasAccess(PermissionsEnum permission)
        {
            return Name == permission.ToString();
        }
    }
    public class Rol : Permission
    {
        private List<Permission> _permissions = new List<Permission>();

        public Rol(string roleName)
        {
            Name = roleName ?? throw new ArgumentNullException(nameof(roleName));
        }

        public override void Add(Permission permission)
        {
            _permissions.Add(permission);
        }

        public override void Remove(Permission permission)
        {
            _permissions.Remove(permission);
        }
        public override bool HasAccess(PermissionsEnum permission)
        {
            foreach (var item in _permissions)
            {
                if (item.HasAccess(permission))
                    return true;
            }
            return false;
        }
    }
    public class SinglePermission : Permission
    {

        public SinglePermission(string permissionName)
        {
            Name = permissionName ?? throw new ArgumentNullException(nameof(permissionName));
        }

        public override void Add(Permission permission)
        {
        }

        public override void Remove(Permission permission)
        {
        }

    }
    public enum LogType
    {
        Informacion,
        Error
    }
}
