using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.API.Dto;
using AuthServer.API.Helpers;

namespace AuthServer.API.Repositories.Book
{
    public interface IBookRepository
    {
        Task<ResponseObject<List<BookTitleDto>>> GetTitles();
    }
}