using System;
using System.IO;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class BackupsRepository
    {
        public async Task<Response<string>> CreateBackup(string folderPath)
        {

            var nameBack = string.Concat(DateTime.Now.ToShortDateString().Replace("/", "-"), "-", Guid.NewGuid(), ".back");
            //var directory = Path.Combine(ConfigurationManagerKeys.Configuration().BackupsFolder, nameBack);
            var directory = Path.Combine(folderPath, nameBack);

            //var directory = Path.Combine(folderPath, nameBack);
            await (new SqlAccessService<Backup>()).ExcecuteQueryAsync($"Backup database LicitProd to disk='{directory}'", new Parameters().Send());
            return Response<string>.Ok(nameBack);
        }
        public async Task<Response<Backup>> RestoreLastBackup(string path)
        {
            await (new SqlAccessService<Backup>()).ExcecuteQueryAsync($"ALTER DATABASE LicitProd  SET OFFLINE WITH ROLLBACK IMMEDIATE RESTORE DATABASE LicitProd FROM DISK = '{path}' WITH REPLACE ALTER DATABASE LicitProd SET ONLINE", new Parameters().Send());
            return Response<Backup>.Ok(default);
        }
    }
}
