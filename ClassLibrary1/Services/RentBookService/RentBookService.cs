using Lesson1_BL.DTOs;
using Lesson1_BL.Services.LibrariesService;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.RentBookService
{
    public class RentBookService : IRentBookService
    {
        private readonly ILibrariesService _librariesService;
        private readonly IRentBookRepository _rentBookRepository;
        private readonly IGenericRepository<RentBook> _genericRentBookRepository;
        public RentBookService(
            ILibrariesService librariesService, 
            IRentBookRepository rentBookRepository,
            IGenericRepository<RentBook> genericRentBookRepository)
        {
            _librariesService = librariesService;
            _rentBookRepository = rentBookRepository;
            _genericRentBookRepository = genericRentBookRepository;
        }

        public async Task<RentBookDto> RentABook(Guid bookId, Guid clientId, Guid libraryId)
        {
            var result = await _rentBookRepository.GetFullInfo(bookId, libraryId);
            if(result.libraryBook == null)
            {
                throw new ArgumentException("The book hasn't found in this library!");
            }
            await _genericRentBookRepository.Add(
                new RentBook
                {
                    LibraryBookId = result.libraryBook.Id,
                    ClientId = clientId,
                    DateGet = DateTime.Now,
                    DateReturn = null
                });

            return MapTupleToRentBookDto(result);
        }

        public async Task<bool> ReturnABook(Guid bookRevisionId, Guid clientId)
        {
            var rentBook = await _genericRentBookRepository.GetByPredicate(rb => rb.ClientId == clientId && rb.LibraryBook.BookRevisionId == bookRevisionId);
            rentBook.DateReturn = DateTime.Now;

            return await _genericRentBookRepository.Update(rentBook);
        }

        private RentBookDto MapTupleToRentBookDto((Book book, BookRevision bookRevision, LibraryBooks libraryBook) result)
        {
            return new RentBookDto
            {
                BookDto = MapBook(result.book),
                BookRevisionDto = MapBookRevision(result.bookRevision),
                LibraryDto = MapLibrary(result.libraryBook.Library)
            };
        }

        private BookDto MapBook(Book book)
        {
            return new BookDto
            {
                BookId = book.Id,
                Title = book.Title,
                Author = book.Author
            };
        }

        private BookRevisionDto MapBookRevision(BookRevision bookRevision)
        {
            return new BookRevisionDto
            {
                Price = bookRevision.Price,
                PagesCount = bookRevision.PagesCount,
                YearOfPublishing = bookRevision.YearOfPublishing
            };
        }

        private LibraryDto MapLibrary(Library library)
        {
            return new LibraryDto
            {
                XCoordinate = library.Location.XCoordinate,
                YCoordinate = library.Location.YCoordinate,
                FullAddress = library.FullAddress,
                CityName = library.City.Name
            };
        }        
    }
}
