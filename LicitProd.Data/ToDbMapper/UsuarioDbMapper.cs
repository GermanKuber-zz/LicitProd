using System.Data;
using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class UsuarioDbMapper : ObjectToDbMapper<Usuario>
    {
        public UsuarioDbMapper() : base("Usuarios")
        {
        }

        protected override void Map()
        {
            Set(x => x.Id).PrimaryKey();
            Set(x => x.Rol).Ignore();
            Set(x => x.HashPassword)
                .Column("Password")
                .Type(SqlDbType.NVarChar);
            Set(x => x.RolId)
                .Column("Rol_Id");
            
        }
    }
}


