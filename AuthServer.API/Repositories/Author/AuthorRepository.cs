﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.API.Database;
using AuthServer.API.Models;
using AuthServer.API.Repositories.BaseRepository;

namespace AuthServer.API.Repositories.Author
{
    public class AuthorRepository : BaseRepository<Models.Author, Guid>, IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //TODO: IMPLEMENT ADD BOOK FUNCTIONALITIES
        public async Task AddBook(BookAuthor entity)
        {
            await _dbContext.BookAuthors.AddAsync(entity);
            await _dbContext.SaveChangesAsync();    
        }

        public async Task AddBooks(List<BookAuthor> entities)
        {
            await _dbContext.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}