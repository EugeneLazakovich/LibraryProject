using Lesson1_BL;
using Lesson1_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService, ILogger<BooksController> logger)
        {
            _booksService = booksService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _booksService.GetAllBooks();
        }

        [HttpGet("{id}")]
        public async Task<Book> GetById(Guid id)
        {
            return await _booksService.GetByIdBook(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            try
            {
                var result = await _booksService.AddBook(book);

                return Created(result.ToString(), book);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Book book)
        {
            try
            {
                book.Id = id;
                var result = await _booksService.UpdateBook(book);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return await _booksService.DeleteByIdBook(id);
        }

        [HttpGet("getByCity")]
        public async Task<IEnumerable<Book>> GetByCity(string cityName)
        {
            return await _booksService.GetBooksByCity(cityName);
        }

        [HttpGet("mostReadableBooks")]
        public async Task<IEnumerable<Book>> GetMostReadableBooks(int top)
        {
            return await _booksService.GetMostReadableBooks(top);
        }
    }
}
