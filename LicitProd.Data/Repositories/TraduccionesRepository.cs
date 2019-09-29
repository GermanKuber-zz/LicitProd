using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class TraduccionesRepository : BaseRepository<TraduccionValue>
    {
        public new async Task<Response<List<TraduccionValue>>> Get(List<Idioma> idiomas)
        {
            var result = (await CreateMapper()).MapList(await SqlAccessService.SelectData("SELECT Tr.Id," +
                                                                                          "Tr.Traduccion," +
                                                                                          "T.KeyValue," +
                                                                                          "Tr.Id_Idioma," +
                                                                                          "Tr.Id_Termino "+
                                                                                          "FROM Traducciones AS Tr " +
                                                                                          "INNER JOIN  Terminos T " +
                                                                                          "ON Tr.Id_Termino = T.Id  " +
                                                                                          $"WHERE Tr.Id_Idioma IN ({ string.Join(",", idiomas.Select(x => x.Id))})"));
            
            return Response<List<TraduccionValue>>.From(result);
        }
        public async Task<Response<TraduccionValue>> UpdateDataAsync(TraduccionValue traduccionValue)
        {
             await SqlAccessService.UpdateAsync(new Parameters()
                 .Add(nameof(TraduccionValue.Traduccion), traduccionValue.Traduccion),
                 new Parameters()
                     .Add(nameof(TraduccionValue.Id), traduccionValue.Id));
             return Response<TraduccionValue>.Ok(traduccionValue);
        }
    }
}