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
}
