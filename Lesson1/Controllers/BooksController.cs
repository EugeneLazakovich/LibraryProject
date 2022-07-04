using Lesson1_BL;
using Lesson1_DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
        public IEnumerable<Book> GetAll()
        {
            return _booksService.GetAllBooks();
        }

        [HttpGet("{id}")]
        public Book GetById(Guid id)
        {
            return _booksService.GetByIdBook(id);
        }

        [HttpPost]
        public IActionResult Add(Book book)
        {
            try
            {
                var result = _booksService.AddBook(book);

                return Created(result.ToString(), book);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Book book)
        {
            try
            {
                book.Id = id;
                var result = _booksService.UpdateBook(book);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            return _booksService.DeleteByIdBook(id);
        }
    }
}
