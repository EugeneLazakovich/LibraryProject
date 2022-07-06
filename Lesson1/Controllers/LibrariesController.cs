using Lesson1_BL;
using Lesson1_DAL;
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
        private readonly ILogger<LocationsController> _logger;
        private readonly ILibrariesService _librariesService;

        public LibrariesController(ILibrariesService librariesService, ILogger<LocationsController> logger)
        {
            _librariesService = librariesService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Library> GetAll()
        {
            return _librariesService.GetAllLibraries();
        }

        [HttpPost]
        public IActionResult Add(Library library)
        {
            try
            {
                var result = _librariesService.AddLibrary(library);

                return Created(result.ToString(), library);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("top")]
        public IEnumerable<Library> GetNearestLibraries(Location location, int top)
        {
            return _librariesService.GetNearestLibraries(location, top);
        }
    }
}

