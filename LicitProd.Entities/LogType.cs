using LicitProd.Services;
using System;
using System.Collections.Generic;

namespace LicitProd.Entities
{
    public enum PermissionsEnum
    {
        Administrador,
        Duenio,
        Comprador,
        Proveedor,
        EditarComprador,
        EditarProveedores,
        ListarProveedores,
        PublicarConcursos,
        ResponderPreguntas,
        OfertarConcurso,
        PreguntarConcurso
    }

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
    public enum LogType
    {
        Informacion,
        Error
    }
}
