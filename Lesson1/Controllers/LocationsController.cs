using Lesson1_BL;
using Lesson1_DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Lesson1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly ILocationsService _locationsService;

        public LocationsController(ILocationsService locationsService, ILogger<LocationsController> logger)
        {
            _locationsService = locationsService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Location> GetAll()
        {
            return _locationsService.GetAllLocations();
        }

        /*[HttpPost]
        public IActionResult Add(Location location)
        {
            try
            {
                var result = _locationsService.AddLocation(location);

                return Created(result.ToString(), location);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}
