using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.API.Helpers;
using AuthServer.API.Models;
using AuthServer.API.Repositories.Author;
using AuthServer.API.Repositories.BaseRepository;
using AuthServer.API.Services.Interfaces;

namespace AuthServer.API.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IBaseRepository<Author, Guid> _authorRepository;
        private readonly IAuthorRepository _authorRepository1;

        public AuthorsService(IBaseRepository<Author, Guid> authorRepository)
        {
            _authorRepository = authorRepository;
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

        public ResponseObject<IQueryable<Author>> GetAsQueryable()
        {
            try
            {
                var response = new ResponseObject<IQueryable<Author>>
                {
                    Data = _authorRepository.GetAsQueryable(),
                    ResponseType = ResponseType.Success,
                    Error = String.Empty
                };
                return response;
            }
            catch (Exception e)
            {
                return new ResponseObject<IQueryable<Author>>
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
                return response;_
            }
            catch (Exception e)
            {
                response.ResponseType = ResponseType.Error;
                response.Error = e.Message;
                return response;
            }
        }
        
        //TODO: IMPLEMENT ADD BOOK FUNCTIONALITIES
        public Task<ResponseObject<bool>> AddBook(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObject<bool>> AddBooks(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}