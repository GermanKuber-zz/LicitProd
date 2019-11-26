using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Seguridad;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task InvitarProveedores(Concurso concurso, List<Proveedor> proveedores)
        {
            concurso.AgregarProveedores(proveedores);
             

            var notificationManager = new NotificationManager();
            var concursoProveedorRepository = new ConcursoProveedorRepository();
            foreach (var proveedor in proveedores)
            {
                await concursoProveedorRepository.InsertDataAsync(new ConcursoProveedor
                {
                    ProveedorId = proveedor.Id,
                    ConcursoId = concurso.Id
                });
            }
            var hitoConcurso = new HitoConcurso
            {
                ConcursoId = concurso.Id,
                Hito = JsonConvert.SerializeObject(concurso)
            };
            await _hitoConcursoRepository.InsertDataAsync(hitoConcurso);

            notificationManager.Notificar(IdentityServices.Instance.GetUserLogged().Email, $"Nuevos proveedores invitados");

        }
        public async Task<Response<PreguntaConcurso>> HacerPregunta(ConcursoProveedor concursoProveedor, string pregunta)
        {
         

            var preguntaConcurso = new PreguntaConcurso
            {
                Pregunta = pregunta,
                ConcursoProveedorId = concursoProveedor.Id
            };
            var response = await new PreguntaRepository().InsertDataAsync(preguntaConcurso);
            new NotificationManager().Notificar(IdentityServices.Instance.GetUserLogged().Email, $"Nuevos proveedores invitados");
            return response;
        }

        public async Task<Response<PreguntaConcurso>> ResponderPregunta(PreguntaConcurso pregunta)
        {
            var response = await new PreguntaRepository().UpdateDataAsync(pregunta);
            return response;
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

        public async Task EstablecerGanador(Concurso concurso, ConcursoProveedor concursoProveedor)
        {
            concursoProveedor.Ganador = true;
            await new ConcursoProveedorRepository().UpdateDataAsync(concursoProveedor);

            concurso.Status = (int) ConcursoStatusEnum.Cerrado;
            await _concursosRepository.UpdateDataAsync(concurso);
            await _hitoConcursoRepository.InsertDataAsync(new HitoConcurso
            {
                ConcursoId = concurso.Id,
                Hito = JsonConvert.SerializeObject(concurso)
            });
        }
        public async Task<Response<Concurso>> GetConcursoParaOfertarAsync(int concursoId)
        {
            Response<Concurso> concurso = await _concursosRepository.GetByIdAsync(concursoId);
            return concurso;
        }

    }
}
