using Lesson1_BL;
using Lesson1_DAL;
using Lesson1_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _clientsService.GetAllClients();
        }

        [HttpGet("{id}")]
        public async Task<Client> GetById(Guid id)
        {
            return await _clientsService.GetByIdClient(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Client client)
        {
            try
            {
                var result = await _clientsService.AddClient(client);

                return Created(result.ToString(), client);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Client client)
        {
            try
            {
                client.Id = id;
                var result = await _clientsService.UpdateClient(client);

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
            return await _clientsService.DeleteByIdClient(id);
        }

        [HttpPut("rent")]
        public async Task<IActionResult> RentABook(Guid bookId, Guid clientId)
        {
            try
            {
                var result = await _clientsService.RentABook(bookId, clientId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("return")]
        public async Task<IActionResult> ReturnABook(Guid bookId, Guid clientId, bool isLost, bool isDamaged, bool isDelayed)
        {
            try
            {
                var result = await _clientsService.ReturnABook(bookId, clientId, isLost, isDamaged);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deposit")]
        public async Task<IActionResult> Deposit(double amount, Guid clientId)
        {
            try
            {
                var result = await _clientsService.Deposit(amount, clientId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
