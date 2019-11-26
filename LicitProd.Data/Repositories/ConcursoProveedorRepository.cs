using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class ConcursoProveedorRepository : BaseRepository<ConcursoProveedor>
    {
        private readonly ProveedoresRepository _proveedoresRepository = new ProveedoresRepository();
        private readonly OfertasRepository _ofertasRepository = new OfertasRepository();
        private readonly PreguntaRepository _preguntaRepository = new PreguntaRepository();
        public async Task<Response<List<ConcursoProveedor>>> GetByConcursoId(int id)
        {
            var result = await GetAsync(new Parameters().Add(nameof(ConcursoProveedor.ConcursoId), id));
            var proveedores = new List<Proveedor>();

            foreach (var item in result.Result)
            {
                var oferta = await _ofertasRepository.GetByConcursoProveedorId(item.Id);
                item.Oferta = oferta.Result;
                proveedores.Add((await _proveedoresRepository.GetByIdAsync(item.ProveedorId)).Result);
                var pregunta =
                    await _preguntaRepository.GetAsync(new Parameters().Add("Concurso_Proveedor_Id", item.Id));
                item.Pregunta = pregunta.Result?.FirstOrDefault();
            }
            
            foreach (var concursoProveedor in result.Result)
                concursoProveedor.Proveedor = proveedores.First(x => x.Id == concursoProveedor.ProveedorId);
            return result;
        }
    }
}
