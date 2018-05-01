using System;
using System.Threading.Tasks;
using AuthServer.API.Dto;
using AuthServer.API.Repositories.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/books")]
    [Authorize(AuthenticationSchemes = "Bearer", Policy="ReadOnly")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET
        [HttpGet("getTitles")]
        public async Task<IActionResult> GetTitles()
        {
            try
            {
                var response = await _bookRepository.GetTitles();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _bookRepository.GetAll();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _bookRepository.GetById(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> Search(string name)
        {
            try
            {
                var response = await _bookRepository.SearchByName(name);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Create([FromBody] BookDto entity)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception(ModelState.Values.ToString());
                return Ok(await _bookRepository.Create(entity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        
        [HttpPost("update")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Update([FromBody] BookDto entity)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception(ModelState.Values.ToString());
                return Ok(await _bookRepository.Update(entity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        
        [HttpPost("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(await _bookRepository.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}