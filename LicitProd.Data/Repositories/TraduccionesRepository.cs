using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class TraduccionesRepository : BaseRepository<TraduccionValue>
    {
        public new async Task<Response<List<TraduccionValue>>> Get(List<Idioma> idiomas)
        {
            var result = (await CreateMapper()).MapList(await SqlAccessService.SelectData("SELECT Tr.Id," +
                                                                                          "Tr.Traduccion," +
                                                                                          "T.[Key]," +
                                                                                          "Tr.Id_Idioma," +
                                                                                          "Tr.Id_Termino "+
                                                                                          "FROM Traducciones AS Tr " +
                                                                                          "INNER JOIN  Terminos T " +
                                                                                          "ON Tr.Id_Termino = T.Id  " +
                                                                                          $"WHERE Tr.Id_Idioma IN ({ string.Join(",", idiomas.Select(x => x.Id))})"));
            
            return Response<List<TraduccionValue>>.From(result);
        }
    }
}