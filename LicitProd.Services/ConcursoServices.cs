using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Seguridad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicitProd.Data.ToDbMapper;
using static LicitProd.Data.ToDbMapper.ConcursoProveedorDbMapper;

namespace LicitProd.Services
{
    public class ConcursoServices : BaseService
    {
        private ConcursosRepository _concursosRepository = new ConcursosRepository();
        private HitoConcursoRepository _hitoConcursoRepository = new HitoConcursoRepository();
        private ProveedoresRepository _proveedoresRepository = new ProveedoresRepository();

        public async Task AceptarTerminosYCondiciones(int concursoId, int usuarioId)
        {
            var proveedor = (await new ProveedoresRepository().GetByUserId(usuarioId));
            await new SqlAccessService<ConcursoProveedor>().UpdateAsync(new Parameters()
                    .Add("AceptoTerminosYCondiciones", true)
                .Add(nameof(ConcursoProveedor.Status), (int)ProveedorConcursoStatusEnum.Aceptado),
                new Parameters()
                    .Add("ProveedorId", proveedor.Result.Id)
                    .Add("ConcursoId", concursoId));

        }
        public async Task RechazarTerminosYCondiciones(int concursoId, int usuarioId)
        {
            var proveedor = (await new ProveedoresRepository().GetByUserId(usuarioId));
            await new SqlAccessService<ConcursoProveedor>().UpdateAsync(new Parameters()
                    .Add("AceptoTerminosYCondiciones", false)
                    .Add(nameof(ConcursoProveedor.Status), (int)ProveedorConcursoStatusEnum.Rechazado),
            new Parameters()
                    .Add("ProveedorId", proveedor.Result.Id)
                    .Add("ConcursoId", concursoId));

        }
        public async Task<Response<Oferta>> RealizarOferta(decimal monto, string detalle, int concursoProveedorId)
        {

            var oferta = new Oferta(monto, detalle, concursoProveedorId);

            return await new OfertasRepository().InsertDataAsync(oferta);
        }
        public async Task<Response<string>> Crear(Concurso concurso, List<Proveedor> proveedores)
        {

            var usuarioId = IdentityServices.Instance.GetUserLogged().Id;

            var comprador = await (new CompradorRepository().GetByUserId(usuarioId));
            concurso.CompradorId = comprador.Result.Id;

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

            notificationManager.Notificar(IdentityServices.Instance.GetUserLogged().Email, $"Nuevo concurso creado con el id : {concurso.Id}");
            return Response<string>.Ok("");
        }

        public async Task<Response<Concurso>> GetConcursoParaOfertarAsync(int concursoId)
        {
            Response<Concurso> concurso = await _concursosRepository.GetByIdAsync(concursoId);
            return concurso;
        }

    }
}
