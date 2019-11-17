using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Data.ToDbMapper;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class ConcursoProveedorRepository : BaseRepository<ConcursoProveedor>
    {
        private readonly ProveedoresRepository _proveedoresRepository = new ProveedoresRepository();
        private readonly OfertasRepository _ofertasRepository = new OfertasRepository();
        public async Task<Response<List<ConcursoProveedor>>> GetByConcursoId(int id)
        {
            var result = await GetAsync(new Parameters().Add(nameof(ConcursoProveedor.ConcursoId), id));
            var proveedores = new List<Proveedor>();

            foreach (var item in result.Result)
            {
                var oferta = await _ofertasRepository.GetByConcursoProveedorId(item.Id);
                item.Oferta = oferta.Result;
                proveedores.Add((await _proveedoresRepository.GetByIdAsync(item.ProveedorId)).Result);

            }
            
            foreach (var concursoProveedor in result.Result)
                concursoProveedor.Proveedor = proveedores.First(x => x.Id == concursoProveedor.ProveedorId);
            return result;
        }
    }
}
