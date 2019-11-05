using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.Seguridad;

namespace LicitProd.Services
{
    public class ProveedoresServices : BaseService
    {
        private ProveedoresRepository _proveedoresRepository = new ProveedoresRepository();

        public async Task<Response<Proveedor>> Registrar(Proveedor proveedor, string email, string password)
        {
            var rol = (await new RolRepository().Get()).Result.FirstOrDefault(x => x.Nombre == "Proveedor");
            proveedor.Usuario = new Usuario(email, new HashService().Hash(password), rol);

            (await _proveedoresRepository.GetByRazonSocial(proveedor.RazonSocial))
                 .Success(async x =>
                 {
                     
                 })
                 .Error(async e=>
                 {
                     var usuarioRepository = new UsuarioRepository();
                     var response = await usuarioRepository.InsertDataAsync(proveedor.Usuario);
                     await _proveedoresRepository.InsertDataAsync(proveedor);
                 });
             return Response<Proveedor>.Ok(proveedor);
        }
    }
}
