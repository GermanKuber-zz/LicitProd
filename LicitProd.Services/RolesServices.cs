using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Data.Repositories;
using LicitProd.Data.ToDbMapper;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class RolesServices : BaseService
    {
        private RolRepository _rolRepository = new RolRepository();
        private GenericRepository<RolPermisoDbMapper.RolPermiso> _rolPermisoRepository;

        public async Task<Response<Rol>> CreatAsync(Rol rol)
        {
            (await _rolRepository.Get(new Parameters()
                    .Add(nameof(rol.Nombre), rol.Nombre)))
                .Error(async x =>
                {
                    await _rolRepository.InsertDataAsync(rol);
                });
            return Response<Rol>.Ok(default);
        }
        public async Task<Response<string>> EliminarAsync(Rol rol)
        {
            if (rol.ByDefault)
                return Response<string>.Error("No se puede eliminar uno de los roles por defecto");
            var rolPermisoRepository = new GenericRepository<RolPermisoDbMapper.RolPermiso>();

            await rolPermisoRepository.DeleteAsync(new Parameters()
                .Add(nameof(RolPermisoDbMapper.RolPermiso.RolId), rol.Id));
            await rolPermisoRepository.DeleteAsync(new Parameters()
                .Add(nameof(RolPermisoDbMapper.RolPermiso.PermisoId), rol.Id));

            await _rolRepository.DeleteAsync(new Parameters().Add(nameof(rol.Id), rol.Id));

            return Response<string>.Ok("");
        }
    }
}