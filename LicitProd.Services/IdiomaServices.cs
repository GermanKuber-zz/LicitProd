using System.Threading.Tasks;
using LicitProd.Data.Repositories;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class IdiomaServices : BaseService
    {
        private IdiomasRepository _idiomasRepository = new IdiomasRepository();

        public async Task<Response<string>> Crear(Idioma idioma)
        {
            var traduccionesRepository = new TraduccionesRepository();
            var nombre = await _idiomasRepository.GetByName(idioma.Nombre);
            if (!nombre.SuccessResult)
            {
                await _idiomasRepository.InsertDataAsync(idioma);

                foreach (var traduccion in idioma.Traducciones)
                {
                    await traduccionesRepository.InsertDataAsync(idioma, traduccion);
                }
            }
            else
                return Response<string>.Error("El nombre que esta queriendo utilizar para el nuevo Idioma, ya existe.");

            return Response<string>.Ok("");
        }
    }
}