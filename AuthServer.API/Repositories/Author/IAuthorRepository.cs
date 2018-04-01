using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.API.Models;

namespace AuthServer.API.Repositories.Author
{
    public interface IAuthorRepository
    {
        Task AddBook(Book entity);
        Task AddBooks(List<Guid> books);
    }
}