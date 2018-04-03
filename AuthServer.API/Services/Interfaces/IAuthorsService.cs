using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.API.Dto;
using AuthServer.API.Helpers;
using AuthServer.API.Models;

namespace AuthServer.API.Services.Interfaces
{
    public interface IAuthorsService
    {
        Task<ResponseObject<List<Author>>> GetAll();
                
        Task<ResponseObject<List<AuthorDto>>> SearchByName(string name);
 
        Task<ResponseObject<Author>> GetById(Guid id);
 
        Task<ResponseObject<bool>> Create(Author entity);
 
        Task<ResponseObject<bool>> Update(Guid id, Author entity);
 
        Task<ResponseObject<bool>> Delete(Guid id);

        Task<ResponseObject<bool>> AddBook(Guid authorId, Guid bookId);

        Task<ResponseObject<bool>> AddBooks(Guid authorId, List<Guid> entities);
    }
}