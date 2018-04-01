using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.API.Database;
using AuthServer.API.Models;
using AuthServer.API.Repositories.BaseRepository;

namespace AuthServer.API.Repositories.Author
{
    public class AuthorRepository : BaseRepository<Models.Author, Guid>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        //TODO: IMPLEMENT ADD BOOK FUNCTIONALITIES
        public Task AddBook(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task AddBooks(List<Guid> books)
        {
            throw new NotImplementedException();
        }
    }
}