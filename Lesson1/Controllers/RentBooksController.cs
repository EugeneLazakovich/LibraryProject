using Lesson1_BL;
using Lesson1_BL.Services.RentBookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        [Authorize(Roles = Roles.Reader)]
        [HttpPut("rent")]
        public async Task<IActionResult> RentABook(Guid bookId, Guid clientId, Guid libraryId)
        {
            try
            {
                var result = await _rentBookService.RentABook(bookId, clientId, libraryId);

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
