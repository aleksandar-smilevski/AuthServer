using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.API.Helpers;
using AuthServer.API.Models;

namespace AuthServer.API.Services.Interfaces
{
    public interface IAuthorsService
    {
        Task<ResponseObject<List<Author>>> GetAll();
                
        ResponseObject<IQueryable<Author>> GetAsQueryable();
 
        Task<ResponseObject<Author>> GetById(Guid id);
 
        Task<ResponseObject<bool>> Create(Author entity);
 
        Task<ResponseObject<bool>> Update(Guid id, Author entity);
 
        Task<ResponseObject<bool>> Delete(Guid id);

        Task<ResponseObject<bool>> AddBook(Book entity);

        Task<ResponseObject<bool>> AddBooks(Book entity);
    }
}