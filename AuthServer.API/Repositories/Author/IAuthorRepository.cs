using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.API.Dto;
using AuthServer.API.Helpers;

namespace AuthServer.API.Repositories.Author
{
    public interface IAuthorRepository
    {
        Task<ResponseObject<List<AuthorDto>>> GetAll();
        Task<ResponseObject<AuthorDto>> GetById(Guid id);
        Task<ResponseObject<List<AuthorDto>>> SearchByName(string name);
        Task<ResponseObject<bool>> Create(AuthorDto author);
        Task<ResponseObject<bool>> Update(AuthorDto author);
        Task<ResponseObject<bool>> Delete(Guid id);
    }
}