using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.API.Database;
using AuthServer.API.Models;
using AuthServer.API.Repositories.BaseRepository;

namespace AuthServer.API.Repositories.Book
{
    public class BookRepository: IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public BookRepository(ApplicationDbContext dbContext)
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

        public Task<List<Models.Book>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.Book> GetAsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<Models.Book> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Create(Models.Book entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, Models.Book entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}