using LicitProd.Data;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class BackupServices : BaseService
    {
        private BackupsRepository _backupsRepository = new BackupsRepository();
        public Response<Backup> CreateBackup() => _backupsRepository.CreateBackup();
        public Response<Backup> RestoreLastBackup() => _backupsRepository.RestoreLastBackup();

    }
}
