using Lesson1_DAL;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1_BL
{
    public class BooksService : IBooksService
    {
        private readonly IGenericRepository<Book> _genericRepository; 
        public BooksService(IGenericRepository<Book> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<Guid> AddBook(Book book)
        {
            return await _genericRepository.Add(book);
        }        

        public async Task<bool> DeleteByIdBook(Guid id)
        {
            return await _genericRepository.DeleteById(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _genericRepository.GetAll();
        }

        public async Task<Book> GetByIdBook(Guid id)
        {
            return await _genericRepository.GetById(id);
        }

        public async Task<bool> UpdateBook(Book book)
        {
            return await _genericRepository.Update(book);
        }

        public async Task<IEnumerable<Book>> GetBooksByCity(string cityName)
        {
            return (await _genericRepository.GetAll());
        }

        public async Task<IEnumerable<Book>> GetMostReadableBooks(int top)
        {
            return (await _genericRepository.GetAll());
        }
    }
}
