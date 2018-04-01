using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.API.Helpers;
using AuthServer.API.Models;
using AuthServer.API.Repositories.Author;
using AuthServer.API.Repositories.BaseRepository;
using AuthServer.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.API.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly AuthorRepository _authorRepository;
        private readonly IBaseRepository<Book, Guid> _bookRepository;

        public AuthorsService(AuthorRepository authorRepository, IBaseRepository<Book, Guid> bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }
        
        public async Task<ResponseObject<List<Author>>> GetAll()
        {
            try
            {
                var response = new ResponseObject<List<Author>>
                {
                    Data = await _authorRepository.GetAll(),
                    ResponseType = ResponseType.Success,
                    Error = String.Empty
                };
                return response;
            }
            catch (Exception e)
            {
                return new ResponseObject<List<Author>>
                {
                    Data = null,
                    ResponseType = ResponseType.Error,
                    Error = e.Message
                };
            }
        }

        public async Task<ResponseObject<List<Author>>> SearchByName(string name)
        {
            var response = new ResponseObject<List<Author>> {Data = new List<Author>()};

            try
            {
                var queryable = _authorRepository.GetAsQueryable();

                var authors = await queryable.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name)).ToListAsync();

                response.ResponseType = ResponseType.Success;
                response.Data = authors;
                return response;
            }
            catch (Exception e)
            {
                return new ResponseObject<List<Author>>
                {
                    Data = null,
                    ResponseType = ResponseType.Error,
                    Error = e.Message
                };
            }
        }

        public async Task<ResponseObject<Author>> GetById(Guid id)
        {
            var response = new ResponseObject<Author> {Data = null};
            
            try
            {
                var author = await _authorRepository.GetById(id);

                if (author == null)
                {
                    response.ResponseType = ResponseType.NoDataFound;
                    response.Error = $"Cannot find {typeof(Author).Name} entity with ID: {id}";
                    return response;
                }

                response.ResponseType = ResponseType.Success;
                response.Data = author;
                return response;
            }
            catch (Exception e)
            {
                response.ResponseType = ResponseType.Error;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ResponseObject<bool>> Create(Author entity)
        {
            var response = new ResponseObject<bool> {Data = false};
            try
            {
                await _authorRepository.Create(entity);
                
                response.Data = true;
                response.ResponseType = ResponseType.Success;
                return response;
            }
            catch (Exception e)
            {
                response.ResponseType = ResponseType.Error;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ResponseObject<bool>> Update(Guid id, Author entity)
        {
            var response = new ResponseObject<bool> {Data = false};
            try
            {
                await _authorRepository.Update(id, entity);
                
                response.Data = true;
                response.ResponseType = ResponseType.Success;
                return response;
            }
            catch (Exception e)
            {
                response.ResponseType = ResponseType.Error;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ResponseObject<bool>> Delete(Guid id)
        {
            var response = new ResponseObject<bool> {Data = false};
            try
            {
                await _authorRepository.Delete(id);
                
                response.Data = true;
                response.ResponseType = ResponseType.Success;
                return response;
            }
            catch (Exception e)
            {
                response.ResponseType = ResponseType.Error;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ResponseObject<bool>> AddBook(Guid authorId, Guid bookId)
        {
            var response = new ResponseObject<bool> {Data = false};
            try
            {
                var book = await _bookRepository.GetById(bookId);

                if (book == null)
                {
                    response.ResponseType = ResponseType.NoDataFound;
                    response.Error = $"Cannot find {typeof(Book).Name} entity with ID: {bookId}";
                    return response;
                }

                var author = await _authorRepository.GetById(authorId);

                if (author == null)
                {
                    response.ResponseType = ResponseType.NoDataFound;
                    response.Error = $"Cannot find {typeof(Author).Name} entity with ID: {authorId}";
                    return response;
                }

                var newEntry = new BookAuthor
                {
                    AuthorId = authorId,
                    BookId = book.Id
                };

                await _authorRepository.AddBook(newEntry);
                response.ResponseType = ResponseType.Success;
                return response;
            }
            catch (Exception e)
            {
                response.ResponseType = ResponseType.Error;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ResponseObject<bool>> AddBooks(Guid authorId, List<Guid> entities)
        {
            var response = new ResponseObject<bool> {Data = false};
            try
            {
                var author = await _authorRepository.GetById(authorId);

                if (author == null)
                {
                    response.ResponseType = ResponseType.NoDataFound;
                    response.Error = $"Cannot find {typeof(Author).Name} entity with ID: {authorId}";
                    return response;
                }
                
                var newEntitiesList = new List<BookAuthor>();
                
                foreach (var entityId in entities)
                {
                    var book = await _bookRepository.GetById(entityId);

                    if (book == null)
                    {
                        response.ResponseType = ResponseType.NoDataFound;
                        response.Error = $"Cannot find {typeof(Book).Name} entity with ID: {entityId}";
                        return response;
                    }

                    var newEntity = new BookAuthor
                    {
                        AuthorId = authorId,
                        BookId = book.Id
                    };
                    
                    newEntitiesList.Add(newEntity);
                }

                await _authorRepository.AddBooks(newEntitiesList);
                response.Data = true;
                response.ResponseType = ResponseType.Success;
                return response;
            }
            catch (Exception e)
            {
                response.ResponseType = ResponseType.Error;
                response.Error = e.Message;
                return response;
            }
        }
    }
}