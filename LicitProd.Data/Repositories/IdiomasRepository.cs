using System;
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
            var traduccionesRepository = new TraduccionesRepository();
            var idiomas = await GetAsync();
            var traducciones = (await traduccionesRepository.Get(idiomas.Result));
            var keys = (await traduccionesRepository.GetAllKeys());
            var keysToInsert = keys.Result.Where(p => !traducciones.Result.GroupBy(f => f.KeyValue).Any(h => h.Key == p.KeyValue));

            foreach (var traduccion in keysToInsert)
            {
                foreach (var idioma in idiomas.Result)
                {
                    try
                    {
                        await traduccionesRepository.InsertDataAsync(idioma, traduccion);

                    }
                    catch (Exception ex)
                    {
                    }
                }
            }


            traducciones = (await traduccionesRepository.Get(idiomas.Result));


            traducciones.Result.GroupBy(x => x.IdiomaId)
                .ToList()
                .ForEach(terminosInIdioma =>
                {
                    var idioma = idiomas.Result.First(x => x.Id == terminosInIdioma.Key);
                    idioma.SetTraducciones(terminosInIdioma.ToList());
                });
            return idiomas;
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