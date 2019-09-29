using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
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
            return Response<Idioma>.Ok(result.Result.First());
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