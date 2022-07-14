using Lesson1_BL;
using Lesson1_BL.Services.RentBookService;
using Lesson1_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentBooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IRentBookService _rentBookService;

        public RentBooksController(IRentBookService rentBookService, ILogger<BooksController> logger)
        {
            _rentBookService = rentBookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<RentBook>> GetAll()
        {
            return await _rentBookService.GetAllRentBooks();
        }

        [HttpGet("{id}")]
        public async Task<RentBook> GetById(Guid id)
        {
            return await _rentBookService.GetByIdRentBook(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RentBook rentBook)
        {
            try
            {
                var result = await _rentBookService.AddRentBook(rentBook);

                return Created(result.ToString(), rentBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RentBook rentBook)
        {
            try
            {
                rentBook.Id = id;
                var result = await _rentBookService.UpdateRentBook(rentBook);

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
            return await _rentBookService.DeleteByIdRentBook(id);
        }

        [Authorize(Roles = Roles.Reader)]
        [HttpPut("rent")]
        public async Task<IActionResult> RentABook(Location location, Guid bookId, Guid clientId, int top)
        {
            try
            {
                var result = await _rentBookService.RentABook(location, bookId, clientId, top);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Roles.Reader)]
        [HttpPut("return")]
        public async Task<IActionResult> ReturnABook(Guid bookRevisionId, Guid clientId)
        {
            try
            {
                var result = await _rentBookService.ReturnABook(bookRevisionId, clientId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
