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
                ReturnResult(CreateMapper().Map(SqlAccessService.SelectData(" with recursivo as" +
                                                            " (" +
                                                            " select  SP2.RolId, SP2.PermisoId from Rol_Permiso SP2" +
                                                            " join Usuario_Rol ur on ur.RolId = sp2.PermisoId" +
                                                            $" where  sp2.RolId is null and ur.UsuarioId = {usuarioId}" +
                                                            " UNION ALL" +
                                                            " select  sp.RolId, sp.PermisoId from Rol_Permiso sp" +
                                                            " join recursivo r on r.PermisoId= sp.RolId" +
                                                            ")" +
                                                            " select * from recursivo r  inner join permiso p on r.PermisoId = p.Id")));
    }
}
