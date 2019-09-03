using LicitProd.Entities;
using LicitProd.Services;

namespace LicitProd.Data
{
    public class RolRepository : BaseRepository<Rol>
    {

        public RolRepository()
        {
        }
        public Response<Rol> GetByUsuarioId(int usuarioId) =>
                ReturnResult(CreateMapper().Map(SqlAccessService.SelectData("with recursivo as (" +
                                                 " select SP2.Id, SP2.RolId, SP2.PermisoId  from Rol_Permiso SP2" +
                                                 " where sp2.RolId = (Select us_rol.Id from Usuarios us" +
                                                 " inner join Usuario_Rol us_rol on us.Id = us_rol.UsuarioId" +
                                                 $" where us.Id = {usuarioId})" +
                                                 " UNION ALL " +
                                                 " select sp.Id, sp.RolId, sp.PermisoId from Rol_Permiso sp " +
                                                 " inner join recursivo r on r.PermisoId= sp.RolId    )" +
                                                 " select *" +
                                                 " from recursivo r " +
                                                 " inner join permiso p on r.PermisoId = p.Id")));
    }
}
