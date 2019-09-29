using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class GenericRepository<TEntity> : BaseRepository<TEntity> where TEntity : IEntityToDb, new()
    {
    }
}