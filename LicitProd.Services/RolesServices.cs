using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Data.Repositories;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class RolesServices : BaseService
    {
        private RolRepository _rolRepository = new RolRepository();

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
    }
}