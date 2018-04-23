using System;
using System.Threading.Tasks;
using AuthServer.API.Repositories.Book;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/books")]
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
    }
}