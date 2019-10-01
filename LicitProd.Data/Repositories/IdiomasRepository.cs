using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class ConfiguracionesRepository : BaseRepository<Configuracion>
    {
        public async Task<Response<Configuracion>> GetByUserId(int userId)
        {
            var result = await GetAsync(new Parameters().Add("Usuario_Id", userId));
            if (result.SuccessResult)
                return Response<Configuracion>.Ok(result.Result.First());
            return Response<Configuracion>.Error();
        }
    }
    public class IdiomasRepository : BaseRepository<Idioma>
    {
        public async Task<Response<List<Idioma>>> Get()
        {
            return (await GetAsync()).Success(async idiomas =>
            {
                (await new TraduccionesRepository().Get(idiomas))
                    .Success(traducciones =>
                    {
                        traducciones.GroupBy(x => x.IdiomaId)
                            .ToList()
                            .ForEach(terminosInIdioma =>
                            {
                                var idioma = idiomas.First(x => x.Id == terminosInIdioma.Key);
                                idioma.SetTraducciones(terminosInIdioma.ToList());
                            });
                    });
            });
        }
        public async Task<Response<List<Idioma>>> GetIdiomaAlone()
        {
            return await GetAsync();
        }
        public async Task<Response<Idioma>> GetByName(string name)
        {
            var result = (await GetAsync(new Parameters().Add("Nombre", name))).Success(async idiomas =>
            {
                (await new TraduccionesRepository().Get(idiomas))
                    .Success(traducciones =>
                    {
                        traducciones.GroupBy(x => x.IdiomaId)
                            .ToList()
                            .ForEach(terminosInIdioma =>
                            {
                                var idioma = idiomas.First(x => x.Id == terminosInIdioma.Key);
                                idioma.SetTraducciones(terminosInIdioma.ToList());
                            });
                    });
            });
            return result.Success(x => Response<Idioma>.Ok(x.First()),
                    () => Response<Idioma>.Error(""))
                .Error(e => Response<Idioma>.Error(e));
        }
        public async Task<Response<Idioma>> UpdateDataAsync(Idioma idioma)
        {
            var traduccionesRepository = new TraduccionesRepository();
            foreach (var traduccion in idioma.Traducciones)
            {
                await traduccionesRepository.UpdateDataAsync(traduccion);
            }
            return Response<Idioma>.Ok(idioma);
        }
    }
}