using System.Threading.Tasks;
using LicitProd.Data.Repositories;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class CompradoresServices : BaseService
    {
        private readonly CompradorRepository _compradorRepository = new CompradorRepository();

        public async Task<Response<Comprador>> Registrar(Comprador comprador)
        {
            var userRepository = new UsuarioRepository();
            
            (await userRepository.GetUsuarioAsync(comprador.Usuario.Email))
                .Success(async x =>
                {

                })
                .Error(async e =>
                {
                    var usuarioRepository = new UsuarioRepository();
                    var response = await usuarioRepository.InsertDataAsync(comprador.Usuario);
                    await _compradorRepository.InsertDataAsync(comprador);
                });
            return Response<Comprador>.Ok(comprador);
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
                     var usuarioRepository = new UsuarioRepository();
                     var response = await usuarioRepository.InsertDataAsync(proveedor.Usuario);
                     await _proveedoresRepository.InsertDataAsync(proveedor);
                 });
             return Response<Proveedor>.Ok(proveedor);
        }
    }
}
