using Lesson1_BL.DTOs;
using Lesson1_BL.Services.LibrariesService;
using Lesson1_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

        [HttpGet("top")]
        public async Task<IEnumerable<NearestLibraryDto>> GetNearestLibraries(Location location, int top)
        {
            return await _librariesService.GetNearestLibraries(location, top);
        }
    }
}

