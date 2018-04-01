using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.API.Database;
using AuthServer.API.Models;
using AuthServer.API.Repositories.BaseRepository;

namespace AuthServer.API.Repositories.Book
{
    public class BookRepository: BaseRepository<Models.Book, Guid>, IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAuthor(BookAuthor entity)
        {
            await _dbContext.BookAuthors.AddAsync(entity);
            await _dbContext.SaveChangesAsync(); 
        }

        public async Task AddAuthors(List<BookAuthor> entities)
        {
            await _dbContext.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}