using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.API.Database;
using AuthServer.API.Dto;
using AuthServer.API.Helpers;
using AuthServer.API.Models;
using AuthServer.API.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.API.Repositories.Book
{
    public class BookRepository: IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ResponseObject<List<BookTitleDto>>> GetTitles()
        {
            var response = new ResponseObject<List<BookTitleDto>> { Data = new List<BookTitleDto>() };
            try
            {
                var data = await _dbContext.Books.Select(x => new BookTitleDto {Id = x.Id, Title = x.Title})
                    .ToListAsync();
                response.ResponseType = ResponseType.Success;
                response.Data = data;
                return response;
            }
            catch (Exception e)
            {
                response.Error = e.Message;
                response.ResponseType = ResponseType.Error;
                return response;
            }
        }
    }
}