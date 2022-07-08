using Lesson1_BL;
using Lesson1_DAL;
using Lesson1_DAL.Models;
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
    public class LibrariesController : ControllerBase
    {
        private readonly ILogger<LibrariesController> _logger;
        private readonly ILibrariesService _librariesService;

        public LibrariesController(ILibrariesService librariesService, ILogger<LibrariesController> logger)
        {
            _librariesService = librariesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Library>> GetAll()
        {
            return await _librariesService.GetAllLibraries();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Library library)
        {
            try
            {
                var result = await _librariesService.AddLibrary(library);

                return Created(result.ToString(), library);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("top")]
        public async Task<IEnumerable<string>> GetNearestLibraries(Location location, int top)
        {
            return (await _librariesService.GetNearestLibraries(location, top)).Select(c => c.FullAddress);
        }
    }
}

