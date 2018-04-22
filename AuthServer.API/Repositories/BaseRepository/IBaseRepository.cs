using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.API.Helpers;

namespace AuthServer.API.Repositories.BaseRepository
{
    public interface IBaseRepository
    {
        Task<ResponseObject<List<T>>> GetAll<T>();
        
        ResponseObject<IQueryable<T>> GetAsQueryable<T>();
 
        ResponseObject<Task<T>> GetById<T>(Guid id);

        Task<ResponseObject<List<T>>> SearchByName<T>(string name); 
            
        Task<ResponseObject<bool>> Create<T>(T entity);
 
        Task<ResponseObject<bool>>  Update<T>(Guid id, T entity);
 
        Task<ResponseObject<bool>>  Delete<T>(Guid id);
    }
    
    public interface IMyInterface2
    {
        T My<T>();
    }
}