using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.API.Dto;
using AuthServer.API.Helpers;

namespace AuthServer.API.Repositories.Book
{
    public interface IBookRepository
    {
        Task<ResponseObject<List<BookDto>>> GetAll();
        Task<ResponseObject<BookDto>> GetById(Guid id);
        Task<ResponseObject<List<BookDto>>> SearchByName(string name);
        Task<ResponseObject<List<BookTitleDto>>> GetTitles();
        Task<ResponseObject<bool>> Create(BookDto bookDto);
        Task<ResponseObject<bool>> Update(BookDto bookDto);
        Task<ResponseObject<bool>> Delete(Guid id);
    }
}