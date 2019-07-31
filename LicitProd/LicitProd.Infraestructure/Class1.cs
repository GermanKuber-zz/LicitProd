using System;
using System.Collections.Generic;

namespace LicitProd.Infraestructure
{
    class Rol : Permiso
    {
        private List<Permiso> _permissions = new List<Permiso>();
        public override bool HasAccess()
        {
            return base.HasAccess();
        }
        public void Add(Permiso permission)
        {
            _permissions.Add(permission);
        }
        public void Remove(Permiso permission)
        {
            _permissions.Remove(permission);
        }
    }
    class SinglePermission : Permiso { }
    abstract class Permiso
    {
        public virtual bool HasAccess()
        {
            return default;
        }
    }
}
