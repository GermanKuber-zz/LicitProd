using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using System.Threading.Tasks;

namespace LicitProd.Services
{
    public class ConcursoServices : BaseService
    {
        private ConcursosRepository _concursosRepository = new ConcursosRepository();

        public Response<string> Crear(Concurso concurso)
        {
            _concursosRepository.Insert(concurso);
            return Response<string>.Ok("");
        }
    }
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
