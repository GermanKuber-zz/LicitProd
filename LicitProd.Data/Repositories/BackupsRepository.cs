using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class BackupsRepository : BaseRepository<Backup>
    {
        public Response<Backup> CreateBackup()
        {
            SqlAccessService.ExcecuteQuery("BackUp ", new Parameters().Send());
            return Response<Backup>.Ok(default);
        }
        public Response<Backup> RestoreLastBackup()
        {
            SqlAccessService.ExcecuteQuery("BackUp ", new Parameters().Send());
            return Response<Backup>.Ok(default);
        }
    }
}
