using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.API.Models;

namespace AuthServer.API.Repositories.Author
{
    public interface IAuthorRepository
    {
        Task AddBook(BookAuthor entity);
        Task AddBooks(List<BookAuthor> entities);
    }
}