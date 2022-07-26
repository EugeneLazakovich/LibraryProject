using AutoFixture;
using NUnit.Framework;
using System.Threading.Tasks;
using Lesson1_DAL.Models;
using Lesson1_BL.Services.BooksService;
using Moq;
using Lesson1_DAL.Interfaces;
using System;

namespace Lesson1_BL.Tests
{
    public class BookServiceTests
    {
        private Fixture _fixture;
        private BooksService _booksService;
        private Mock<IGenericRepository<Book>> _genericBookRepositoryMock;
        private Mock<IBooksRepository> _booksRepository;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _genericBookRepositoryMock = new Mock<IGenericRepository<Book>>();
            _booksRepository = new Mock<IBooksRepository>();
            _booksService = new BooksService(_genericBookRepositoryMock.Object, _booksRepository.Object);
        }

        [Test]
        public async Task AddBook_WhenCalled_ShouldCallAddOnRepository()
        {
            var book = _fixture.Create<Book>();
            var expectedGuid = Guid.NewGuid();
            _genericBookRepositoryMock.Setup(x => x.Add(book)).ReturnsAsync(expectedGuid).Verifiable();

            var actualGuid = await _booksService.AddBook(book);

            Assert.AreEqual(expectedGuid, actualGuid);
            _genericBookRepositoryMock.Verify();
        }
    }
}