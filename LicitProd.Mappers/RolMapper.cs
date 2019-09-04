using LicitProd.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace LicitProd.Mappers
{
    public class RolMapper : DbToObjectMapper<Rol>
    {
        public Rol Map(DataTable dataTable)
        {
            var permissions = new List<(int ParentRolId, SinglePermission Permission)>();
            var roles = new List<(int? ParentRolId, Rol Rol)>();
            foreach (DataRow item in dataTable.Rows)
            {
                if (item["Type"].ToString() == "Permiso")
                    permissions.Add((int.Parse(item["RolId"].ToString()),
                                     new SinglePermission(int.Parse(item["PermisoId"].ToString()),
                                                          item["Nombre"].ToString())));
                else if (item["Type"].ToString() == "Rol")
                    roles.Add((TryParse(item["RolId"].ToString()),
                                        new Rol(int.Parse(item["Id"].ToString()),
                                                item["Nombre"].ToString())));
            }
            permissions.ForEach(permission =>
                roles.FirstOrDefault(x => x.Rol.Id == permission.ParentRolId).Rol.Add(permission.Permission));

            roles.Where(x => x.ParentRolId != null)
                 .ToList()
                 .ForEach(rolEach => roles.FirstOrDefault(x => x.Rol.Id == rolEach.ParentRolId).Rol.Add(rolEach.Rol));

            return roles.FirstOrDefault(x => x.ParentRolId == null).Rol;
        }

        public List<Rol> MapList(DataTable dataTable)
        {
            throw new System.NotImplementedException();
        }
        private int? TryParse(string value)
        {
            if (int.TryParse(value, out int tryValue))
                return tryValue;
            return null;
        }
    }
}
