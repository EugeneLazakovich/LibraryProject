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
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService, ILogger<ClientsController> logger)
        {
            _clientsService = clientsService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Client> GetAll()
        {
            return _clientsService.GetAllClients();
        }

        [HttpGet("{id}")]
        public Client GetById(Guid id)
        {
            return _clientsService.GetByIdClient(id);
        }

        [HttpPost]
        public IActionResult Add(Client client)
        {
            try
            {
                var result = _clientsService.AddClient(client);

                return Created(result.ToString(), client);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Client client)
        {
            try
            {
                client.Id = id;
                var result = _clientsService.UpdateClient(client);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            return _clientsService.DeleteByIdClient(id);
        }

        [HttpPut("rent")]
        public IActionResult RentABook(Guid bookId, Guid clientId)
        {
            try
            {
                var result = _clientsService.RentABook(bookId, clientId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("return")]
        public IActionResult ReturnABook(Guid bookId, Guid clientId)
        {
            try
            {
                var result = _clientsService.ReturnABook(bookId, clientId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deposit")]
        public IActionResult Deposit(double amount, Guid clientId)
        {
            try
            {
                var result = _clientsService.Deposit(amount, clientId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
