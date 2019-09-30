using System.Threading.Tasks;
using LicitProd.Data.Repositories;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class BackupServices : BaseService
    {
        private BackupsRepository _backupsRepository = new BackupsRepository();
        public async Task<Response<string>> CreateBackup(string folderPath) => await _backupsRepository.CreateBackup(folderPath);
        public Response<Backup> RestoreLastBackup() => _backupsRepository.RestoreLastBackup();

    }
}
