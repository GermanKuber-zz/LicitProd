using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using static LicitProd.Data.ToDbMapper.ConcursoProveedorDbMapper;

namespace LicitProd.Services
{
    public class ConcursoServices : BaseService
    {
        private ConcursosRepository _concursosRepository = new ConcursosRepository();
        private HitoConcursoRepository _hitoConcursoRepository = new HitoConcursoRepository();
        public async Task<Response<string>> Crear(Concurso concurso, List<Proveedor> proveedores)
        {
            await _concursosRepository.Insert(concurso);

            var notificationManager = new NotificationManager();
            foreach (var proveedor in proveedores)
            {
                await new SqlAccessService<ConcursoProveedor>().InsertDataAsync(new Parameters()
                  .Add("ProveedorId", proveedor.Id)
                  .Add("ConcursoId", concurso.Id),
                  "Concurso_Proveedor");
            }
            var hitoConcurso = new HitoConcurso
            {
                ConcursoId = concurso.Id,
                Hito = JsonConvert.SerializeObject(concurso)
            };
            await _hitoConcursoRepository.InsertDataAsync(hitoConcurso);
            return Response<string>.Ok("");
        }
}
}
