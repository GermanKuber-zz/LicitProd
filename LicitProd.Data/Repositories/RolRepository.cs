using System.Collections.Generic;
using System.Threading.Tasks;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class RolRepository : BaseRepository<Rol>
    {

        public RolRepository()
        {
        }
        public async Task<Response<Rol>> GetByUsuarioIdAsync(int usuarioId)
        {
            var query = await SqlAccessService.SelectData(" with recursivo as" +
                                                            " (" +
                                                            " select  SP2.RolId, SP2.PermisoId from Rol_Permiso SP2" +
                                                            " join Usuario_Rol ur on ur.RolId = sp2.PermisoId" +
                                                            $" where  sp2.RolId is null and ur.UsuarioId = {usuarioId}" +
                                                            " UNION ALL" +
                                                            " select  sp.RolId, sp.PermisoId from Rol_Permiso sp" +
                                                            " join recursivo r on r.PermisoId= sp.RolId" +
                                                            ")" +
                                                            " select * from recursivo r  inner join permiso p on r.PermisoId = p.Id");
            var map = (await CreateMapper()).Map(query);
            return ReturnResult(map);
        }
        public async Task<Response<List<Rol>>> GetAllAsync() =>
             ReturnResult((await CreateMapper()).MapList((await SqlAccessService.SelectData("with recursivo as ( select  SP2.RolId, SP2.PermisoId from Rol_Permiso  SP2  where   sp2.RolId is null  UNION ALL select  sp.RolId, sp.PermisoId from Rol_Permiso sp join recursivo r on r.PermisoId= sp.RolId) select * from recursivo r  inner join permiso p on r.PermisoId = p.Id"))));
    }
}
