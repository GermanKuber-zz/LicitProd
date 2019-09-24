using System.Threading.Tasks;
using LicitProd.Data.Repositories;
using LicitProd.Entities;

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
    public class ProveedoresServices : BaseService
    {
        private ProveedoresRepository _proveedoresRepository = new ProveedoresRepository();

        public async Task<Response<Proveedor>> Registrar(Proveedor proveedor)
        {
             (await _proveedoresRepository.GetByRazonSocial(proveedor.RazonSocial))
                 .Success(async x =>
                 {
                     
                 })
                 .Error(async e=>
                 {
                     await _proveedoresRepository.InsertDataAsync(proveedor);
                 });
             return Response<Proveedor>.Ok(proveedor);
        }
    }
}
