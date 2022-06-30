using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private static List<Book> Books { get; set; }

        static BookController()
        {
            Books = new List<Book>();
        }

        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return Books;
        }

        [HttpGet("{id}")]
        public Book GetById(Guid id)
        {
            return Books.Where(c => c.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create(Book book)
        {
            book.Id = Guid.NewGuid();
            Books.Add(book);
        }

        [HttpPut("{id}")]
        public bool Update(Guid id, Book book)
        {
            Book bookFromList = GetById(id);
            book.Id = id;
            if (bookFromList != null)
            {
                var index = Books.IndexOf(bookFromList);
                Books[index] = book;
            }
            return bookFromList != null;
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var book = GetById(id);
            Books.Remove(book);
        }
    }
}
