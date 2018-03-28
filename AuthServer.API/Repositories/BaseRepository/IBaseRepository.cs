using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API.Repositories.BaseRepository
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
 
        Task<TEntity> GetById(TKey id);
 
        Task Create(TEntity entity);
 
        Task Update(TKey id, TEntity entity);
 
        Task Delete(TKey id);
    }
}