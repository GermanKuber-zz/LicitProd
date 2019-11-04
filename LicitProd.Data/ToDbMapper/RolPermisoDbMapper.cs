using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class RolPermisoDbMapper : ObjectToDbMapper<RolPermisoDbMapper.RolPermiso>
    {
        public RolPermisoDbMapper() : base("Rol_Permiso")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
        }
        public class RolPermiso : Entity
        {
            public int PermisoId { get; set; }
            public int RolId { get; set; }
        }
    }
}