using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.API.Models;

namespace AuthServer.API.Repositories.Book
{
    public interface IBookRepository 
    {
        Task AddAuthor(BookAuthor entity);
        Task AddAuthors(List<BookAuthor> entities);
    }
}