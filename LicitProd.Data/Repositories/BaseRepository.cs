namespace LicitProd.Data
{
    public class BaseRepository
    {
        protected readonly SqlAccessService SqlAccessService;

        public BaseRepository()
        {
            SqlAccessService = new SqlAccessService();

        }
    }
}
