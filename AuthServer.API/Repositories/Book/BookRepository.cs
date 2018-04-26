using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.API.Database;
using AuthServer.API.Dto;
using AuthServer.API.Helpers;
using AuthServer.API.Models;
using AutoMapper;
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


        public async Task<ResponseObject<List<BookDto>>> GetAll()
        {
            try
            {
                var books = await _dbContext.Books.Include(x => x.Authors).ThenInclude(x => x.Author).ToListAsync();

                var bookDtos = Mapper.Map<List<BookDto>>(books);

                return new ResponseObject<List<BookDto>>
                {
                    Data = bookDtos,
                    ResponseType = ResponseType.Success
                };
            }
            catch (Exception e)
            {
                return new ResponseObject<List<BookDto>>
                {
                    Data = null,
                    ResponseType = ResponseType.Error,
                    Error = e.Message
                };
            }
        }

        public async Task<ResponseObject<BookDto>> GetById(Guid id)
        {
            var response = new ResponseObject<BookDto>{Data = null};

            try
            {
                var book = await _dbContext.Books.Include(x => x.Authors).ThenInclude(x => x.Author).Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (book == null)
                {
                    response.ResponseType = ResponseType.NoDataFound;
                    response.Error = $"Entity not found: Key used: {id}";
                    return response;
                }

                var bookDto = Mapper.Map<BookDto>(book);

                response.Data = bookDto;
                response.ResponseType = ResponseType.Success;
                return response;
            }
            catch (Exception e)
            {
                response.Error = e.Message;
                response.ResponseType = ResponseType.Error;
                return response;
            }
        }

        public async Task<ResponseObject<List<BookDto>>> SearchByName(string name)
        {
            var response = new ResponseObject<List<BookDto>>{Data = new List<BookDto>()};

            try
            {
                var queryable = _dbContext.Books.AsNoTracking();
                
                var books = await queryable.Include(x => x.Authors).ThenInclude(x => x.Author)
                    .Where(x => x.Title.Contains(name))
                    .ToListAsync();
                
                if (!books.Any())
                {
                    response.Error = $"Entity not found. Key used: {name}";
                    response.ResponseType = ResponseType.NoDataFound;
                    return response;
                }
                
                var bookDtos = Mapper.Map<List<BookDto>>(books);

                response.ResponseType = ResponseType.Success;
                response.Data = bookDtos;
                return response;
            }
            catch (Exception e)
            {
                response.Error = e.Message;
                response.ResponseType = ResponseType.Error;
                return response;
            }
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

        public async Task<ResponseObject<bool>> Create(BookDto bookDto)
        {
            var response = new ResponseObject<bool>{Data = false};

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var book = Mapper.Map<Models.Book>(bookDto);
                    await _dbContext.Books.AddAsync(book);
                    await _dbContext.SaveChangesAsync();

                    foreach (var authorDto in bookDto.Authors)
                    {
                        var author = await _dbContext.Authors.Where(x => x.Id == authorDto.Id)
                            .FirstOrDefaultAsync();
                        if (author != null)
                        {
                            var joinEntity = new BookAuthor
                            {
                                AuthorId = author.Id,
                                BookId = book.Id
                            };
                            await _dbContext.BookAuthors.AddAsync(joinEntity);
                            continue;
                        }

                        var newAuthor = Mapper.Map<Models.Author>(authorDto);
                        await _dbContext.Authors.AddAsync(newAuthor);

                        var newJoinEntity = new BookAuthor
                        {
                            AuthorId = newAuthor.Id,
                            BookId = book.Id
                        };

                        await _dbContext.BookAuthors.AddAsync(newJoinEntity);
                    }

                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    response.Data = true;
                    response.ResponseType = ResponseType.Success;
                    return response;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    response.Error = e.Message;
                    response.ResponseType = ResponseType.Error;
                    return response;
                }
            }
        }

        public async Task<ResponseObject<bool>> Update(BookDto bookDto)
        {
            var response = new ResponseObject<bool>();
            
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var book = await _dbContext.Books.Include(x => x.Authors).Where(x => x.Id == bookDto.Id).FirstOrDefaultAsync();
                    if (book == null)
                    {
                        response.Error =  response.Error = $"Entity not found. Key used: {bookDto?.Id}";
                        response.ResponseType = ResponseType.NoDataFound;
                        return response;
                    }

                    book.DatePublished = bookDto.DatePublished;
                    book.Description = bookDto.Description;
                    book.ISBN = bookDto.ISBN;
                    book.Title = bookDto.Title;

                    _dbContext.Books.Update(book);
                    await _dbContext.SaveChangesAsync();
                    
                    transaction.Commit();
                    response.Data = true;
                    response.ResponseType = ResponseType.Success;
                    return response;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    response.Error = e.Message;
                    response.ResponseType = ResponseType.Error;
                    return response;
                }
            }
        }

        public async Task<ResponseObject<bool>> Delete(Guid id)
        {
            var response = new ResponseObject<bool>{Data = false};
            try
            {
                var book = await _dbContext.Books.Include(x => x.Authors).Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (book == null)
                {
                    response.ResponseType = ResponseType.Error;
                    response.Error = $"Entity not found. Key used: {id}";
                    return response;
                }

                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();

                response.ResponseType = ResponseType.Success;
                response.Data = true;
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