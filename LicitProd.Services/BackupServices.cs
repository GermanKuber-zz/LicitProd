using System.Threading.Tasks;
using LicitProd.Data.Repositories;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class ConsistencyValidator
    {

        public async Task<Response<string>> Validate()
        {
            var validator = new ConcursoValidator();
            return await validator.Validate();

        }

    }

    public interface IDitigoVerificadorVerticalValidator
    {
        Task<Response<string>> Validate();
    }

    public class ConcursoValidator : IDitigoVerificadorVerticalValidator
    {
        protected IDitigoVerificadorVerticalValidator Next { get; }

        public ConcursoValidator(IDitigoVerificadorVerticalValidator next)
        {
            Next = next;
        }
        public ConcursoValidator()
        {
        }
        public async Task<Response<string>> Validate()=>
            await (await new DigitoVerificadorRepository().IsValid<ConcursoVerificable>(DigitoVerificadorTablasEnum
                    .Concursos)).Map(async b => await Task.FromResult(Response<string>.Ok("Concursos")),
                    async list => await Task.FromResult(Response<string>.Error("Concursos")));
    }

    public class BackupServices : BaseService
    {
        private BackupsRepository _backupsRepository = new BackupsRepository();
        public Response<Backup> CreateBackup() => _backupsRepository.CreateBackup();
        public Response<Backup> RestoreLastBackup() => _backupsRepository.RestoreLastBackup();

    }
}
