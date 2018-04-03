using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.API.Dto;
using AuthServer.API.Helpers;
using AuthServer.API.Models;
using AuthServer.API.Repositories;
using AuthServer.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<ResponseObject<List<Author>>> GetAll()
        {
            try
            {
                return await _authorsService.GetAll();
            }
            catch (Exception e)
            {
                return new ResponseObject<List<Author>>
                {
                    Data = null,
                    Error = e.Message,
                    ResponseType = ResponseType.Error
                };
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ResponseObject<Author>> GetById(Guid id)
        {
            try
            {
                return await _authorsService.GetById(id);
            }
            catch (Exception e)
            {
                return new ResponseObject<Author>
                {
                    Data = null,
                    Error = e.Message,
                    ResponseType = ResponseType.Error
                };
            }
        }
        
        [HttpGet("search/{name}")]
        public async Task<ResponseObject<List<AuthorDto>>> SearchByName(string name)
        {
            try
            {
                return await _authorsService.SearchByName(name);
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
        
        [HttpPost]
        public async Task<ResponseObject<bool>> Create(Author entity)
        {
            try
            {
                return await _authorsService.Create(entity);
            }
            catch (Exception e)
            {
                return new ResponseObject<bool>
                {
                    Data = false,
                    Error = e.Message,
                    ResponseType = ResponseType.Error
                };
            }
        }
        
        [HttpPost("addBook")]
        public async Task<ResponseObject<bool>> AddBook(Guid authorId, Guid bookId)
        {
            try
            {
                return await _authorsService.AddBook(authorId, bookId);
            }
            catch (Exception e)
            {
                return new ResponseObject<bool>
                {
                    Data = false,
                    Error = e.Message,
                    ResponseType = ResponseType.Error
                };
            }
        }
        
        [HttpPost("addBooks")]
        public async Task<ResponseObject<bool>> AddBooks(Guid authorId, List<Guid> bookIds)
        {
            try
            {
                return await _authorsService.AddBooks(authorId, bookIds);
            }
            catch (Exception e)
            {
                return new ResponseObject<bool>
                {
                    Data = false,
                    Error = e.Message,
                    ResponseType = ResponseType.Error
                };
            }
        }
        
        [HttpPost("update")]
        public async Task<ResponseObject<bool>> Update(Guid id, Author entity)
        {
            try
            {
                return await _authorsService.Update(id, entity);
            }
            catch (Exception e)
            {
                return new ResponseObject<bool>
                {
                    Data = false,
                    Error = e.Message,
                    ResponseType = ResponseType.Error
                };
            }
        }
        
        [HttpDelete("delete")]
        public async Task<ResponseObject<bool>> Delete(Guid id)
        {
            try
            {
                return await _authorsService.Delete(id);
            }
            catch (Exception e)
            {
                return new ResponseObject<bool>
                {
                    Data = false,
                    Error = e.Message,
                    ResponseType = ResponseType.Error
                };
            }
        }
    }
}