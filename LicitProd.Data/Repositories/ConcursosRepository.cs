using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;
using LicitProd.Infrastructure;

namespace LicitProd.Data.Repositories
{
    public class GenericRepository<TEntity> : BaseRepository<TEntity> where TEntity : IEntityToDb, new()
    {
    }

    public class ConcursosRepository : BaseRepository<Concurso>
    {
        public new Task<Response<List<Concurso>>> Get() =>
            base.GetAsync();
        public async Task<Concurso> Insert(Concurso concurso)
        {
            await SqlAccessService.InsertDataAsync(concurso);
            (await new GenericRepository<Verificable>().GetAsync())
                .Success(async x =>
                {
                    var codigoVerificador = VerificableService.GetCheckDigit(string.Join("", x.Select(s => VerificableService.GetCheckDigit(s.Hash))));
                    var digitoVerificadorRepository = new GenericRepository<DigitoVerificadorVertical>();
                    (await digitoVerificadorRepository.GetAsync(
                        new Parameters().Add(nameof(DigitoVerificadorVertical.Tabla), "Concursos")))
                        .Success(async verificadorVertical =>
                        {
                            var valid = verificadorVertical.First();
                            valid.Digito = codigoVerificador;
                            await digitoVerificadorRepository.UpdateDataAsync(valid);
                        })
                        .Error(async erros =>
                        {
                            await digitoVerificadorRepository.InsertDataAsync(new DigitoVerificadorVertical(codigoVerificador,
                                DigitoVerificadorTablasEnum.Concursos));
                        });
                });

            return concurso;
        }
    }
}
