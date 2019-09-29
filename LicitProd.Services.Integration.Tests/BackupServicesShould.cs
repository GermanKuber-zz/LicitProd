using LicitProd.Data.Repositories;

namespace LicitProd.Services.Integration.Tests
{

    public class BackupServicesShould : IntegrationTestsBase
    {
        private BackupServices _sut;
        private BackupsRepository _backupsRepository;

        public BackupServicesShould()
        {
            _sut = new BackupServices();
            _backupsRepository = new BackupsRepository();
        }

    }
}