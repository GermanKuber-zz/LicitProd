using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;
using LicitProd.Infrastructure;
using LicitProd.Seguridad;

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
            await new DigitoVerificadorRepository().Validate<ConcursoVerificable>(
                DigitoVerificadorTablasEnum.Concursos);
            return concurso;
        }
    }

    public class DigitoVerificadorRepository
    {
        public async Task Validate<TEntity>(DigitoVerificadorTablasEnum tabla) where TEntity : Verificable, IEntityToDb, new()
        {
            (await new GenericRepository<TEntity>().GetAsync())
                .Success(async x =>
                {
                    var hasService = new HashService();
                    var codigoVerificador = hasService.Hash(string.Join("", x.Select(s => hasService.Hash(s.Hash))));
                    var digitoVerificadorRepository = new GenericRepository<DigitoVerificadorVertical>();
                    (await digitoVerificadorRepository.GetAsync(
                            new Parameters().Add(nameof(tabla), tabla.ToString())))
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
        }

        public async Task<Response<bool>> IsValid<TEntity>(DigitoVerificadorTablasEnum tabla) where TEntity : Verificable, IEntityToDb, new()
        {
            return await CallVarificables<TEntity>(tabla, (vertical, codigoVerificador) =>
            {
                if (vertical.Digito == codigoVerificador)
                    return Task.FromResult(Response<bool>.Ok(true));
                return Task.FromResult(Response<bool>.Error());
            }, errors => Task.FromResult(Response<bool>.Error(errors)));
        }

        private async Task<Response<bool>> CallVarificables<TEntity>(DigitoVerificadorTablasEnum tabla,
            Func<DigitoVerificadorVertical, string, Task<Response<bool>>> successCall,
            Func<List<string>, Task<Response<bool>>> errorCall
        ) where TEntity : Verificable, IEntityToDb, new()
        {
            return (await new GenericRepository<TEntity>().GetAsync())
                .Success(async x =>
                {
                    var hasService = new HashService();
                    var codigoVerificador = hasService.Hash(string.Join("", x.Select(s => hasService.Hash(s.Hash))));
                    var digitoVerificadorRepository = new GenericRepository<DigitoVerificadorVertical>();
                    return (await digitoVerificadorRepository.GetAsync(
                            new Parameters().Add(nameof(tabla), tabla.ToString())))
                        .Success(async verificadorVertical => await successCall(verificadorVertical.First(), codigoVerificador))
                        .Error(async g => await errorCall(g));
                });

        }
    }
}
