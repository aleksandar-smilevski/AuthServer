using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API.Repositories.BaseRepository
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        
        IQueryable<TEntity> GetAsQueryable();
 
        Task<TEntity> GetById(TKey id);
 
        Task Create(TEntity entity);
 
        Task Update(TKey id, TEntity entity);
 
        Task Delete(TKey id);
    }
}