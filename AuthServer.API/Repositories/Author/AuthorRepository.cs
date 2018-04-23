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

namespace AuthServer.API.Repositories.Author
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public AuthorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ResponseObject<List<AuthorDto>>> GetAll()
        {
            try
            {
                var authors = await _dbContext.Authors.Include(x => x.Books).ThenInclude(x => x.Book).ToListAsync();

                var authorsDto = Mapper.Map<List<AuthorDto>>(authors);
                return new ResponseObject<List<AuthorDto>>
                {
                    Data = authorsDto,
                    Error = null,
                    ResponseType = ResponseType.Success
                };
            }
            catch (Exception e)
            {
                return new ResponseObject<List<AuthorDto>>
                {
                    Data = null,
                    Error = e.Message,
                    ResponseType = ResponseType.Error
                };
            }
        }

        public async Task<ResponseObject<AuthorDto>> GetById(Guid id)
        {
            var response = new ResponseObject<AuthorDto>() {Data = null};
            try
            {
                var author = await _dbContext.Authors.Include(x => x.Books).ThenInclude(x => x.Book).Where(x => x.Id == id).FirstOrDefaultAsync();

                if (author == null)
                {
                    response.ResponseType = ResponseType.NoDataFound;
                    response.Error = $"Entity not found: Key used: {id}";
                    return response;
                }

                var authorDto = Mapper.Map<AuthorDto>(author);

                response.ResponseType = ResponseType.Success;
                response.Data = authorDto;
                return response;
            }
            catch (Exception e)
            {
                response.Error = e.Message;
                response.ResponseType = ResponseType.Error;
                return response;
            }
        }

        public async Task<ResponseObject<List<AuthorDto>>> SearchByName(string name)
        {
            var response = new ResponseObject<List<AuthorDto>>{Data = new List<AuthorDto>()};
            try
            {
                var queryable = _dbContext.Authors.AsNoTracking();

                var authors = await queryable.Include(x => x.Books).ThenInclude(x => x.Book)
                    .Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name))
                    .ToListAsync();

                if (!authors.Any())
                {
                    response.Error = $"Entity not found. Key used: {name}";
                    response.ResponseType = ResponseType.NoDataFound;
                }

                var authorDtos = Mapper.Map<List<AuthorDto>>(authors);

                response.ResponseType = ResponseType.Success;
                response.Data = authorDtos;
                return response;
            }
            catch (Exception e)
            {
                response.Error = e.Message;
                response.ResponseType = ResponseType.Error;
                return response;
            }
        }

        public async Task<ResponseObject<bool>> Create(AuthorDto authorDto)
        {
            var response = new ResponseObject<bool>{Data = false};

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var author = Mapper.Map<Models.Author>(authorDto);
                    await _dbContext.Authors.AddAsync(author);
                    await _dbContext.SaveChangesAsync();

                    foreach (var bookDto in authorDto.Books)
                    {
                        Models.Book newBook;
                        BookAuthor newJoinEntity;

                        if (bookDto.Id != Guid.Empty)
                        {
                            var book = await _dbContext.Books.Where(x => x.Id == bookDto.Id)
                                .FirstOrDefaultAsync();
                            if (book != null)
                            {
                                var joinEntity = new BookAuthor
                                {
                                    AuthorId = author.Id,
                                    BookId = book.Id
                                };
                                await _dbContext.BookAuthors.AddAsync(joinEntity);
                                continue;
                            }

                            newBook = Mapper.Map<Models.Book>(bookDto);
                            await _dbContext.Books.AddAsync(newBook);

                            newJoinEntity = new BookAuthor
                            {
                                AuthorId = author.Id,
                                BookId = newBook.Id
                            };

                            await _dbContext.BookAuthors.AddAsync(newJoinEntity);
                        }

                        newBook = Mapper.Map<Models.Book>(bookDto);
                        await _dbContext.Books.AddAsync(newBook);

                        newJoinEntity = new BookAuthor
                        {
                            AuthorId = author.Id,
                            BookId = newBook.Id
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

        public async Task<ResponseObject<bool>> Update(AuthorDto entity)
        {
            var response = new ResponseObject<bool>{Data = false};
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var author = await _dbContext.Authors.Include(x => x.Books).Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                    if (author == null)
                    {
                        response.Error =  response.Error = $"Entity not found. Key used: {entity?.Id}";
                        response.ResponseType = ResponseType.NoDataFound;
                        return response;
                    }

                    author.FirstName = entity.FirstName;
                    author.LastName = entity.LastName;
                    _dbContext.Authors.Update(author);
                    await _dbContext.SaveChangesAsync();

                    #region commented UPDATE
                    //SAKAM DA GI ZACUVAM NOVITE BOOKS CIE ID = null ili ne postoi vo baza
                    //ZA VEKE POSTOECKITE PROVERI DALI NEKOJ OD ZACUVANITE NE E PRATEN -> toa znaci deka treba da se izbrise
                    // SET A = { 1, 2, 3 } & B = { 5, 6 }
                    //
                    //TODO: TRY UPDATING OF BOOKS AS WELL
//                    foreach (var book in author.Books)
//                    {
//                        if (book.BookId != null)
//                        {
//                            var contains = entity.Books.Any(x => Guid.Parse(x.Id) == book.BookId);
//                            if (contains) continue;
//                        
//                            _dbContext.BookAuthors.Remove(book);
//                        }
//                    }
//                    
//                    await _dbContext.SaveChangesAsync();
//                    
//                    foreach (var bookDto in entity.Books)
//                    {
//                        Models.Book newBook;
//                        BookAuthor newJoinEntity;
//                        
//                        if (bookDto.Id != null)
//                        {
//                            var book = await _dbContext.Books.Where(x => x.Id == Guid.Parse(bookDto.Id)).FirstOrDefaultAsync();
//                            if (book == null)
//                            {
//                                newBook = Mapper.Map<Models.Book>(bookDto);
//                                await _dbContext.Books.AddAsync(newBook);
//
//                                newJoinEntity = new BookAuthor
//                                {
//                                    AuthorId = author.Id,
//                                    BookId = newBook.Id
//                                };
//
//                                await _dbContext.BookAuthors.AddAsync(newJoinEntity);
//                                continue;
//                            }
//                        }
//
//                        newBook = Mapper.Map<Models.Book>(bookDto);
//                        await _dbContext.Books.AddAsync(newBook);
//
//                        newJoinEntity = new BookAuthor
//                        {
//                            AuthorId = author.Id,
//                            BookId = newBook.Id
//                        };
//
//                        await _dbContext.BookAuthors.AddAsync(newJoinEntity);
//                    }
                    //await _dbContext.SaveChangesAsync();
                        #endregion
                    
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
                var author = await _dbContext.Authors.Include(x => x.Books).Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (author == null)
                {
                    response.ResponseType = ResponseType.Error;
                    response.Error = $"Entity not found. Key used: {id}";
                    return response;
                }

                _dbContext.Authors.Remove(author);
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