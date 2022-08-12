﻿using Lesson1_BL.DTOs;
using Lesson1_BL.Services.AuthService;
using Lesson1_BL.Services.UsersService;
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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _clientsService;
        private readonly IAuthService _authService;

        public UsersController(IUsersService clientsService, IAuthService authService, ILogger<UsersController> logger)
        {
            _clientsService = clientsService;
            _authService = authService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _clientsService.GetAllClients();
        }

        [HttpGet("{id}")]
        public async Task<User> GetById(Guid id)
        {
            return await _clientsService.GetByIdClient(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User client)
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

        [HttpGet("signin")]
        public async Task<IActionResult> SignIn(string login, string password)
        {
            string token = null;
            try
            {
                token = await _authService.SignIn(login, password);
            }
            catch (ArgumentException)
            {
            }

            return !String.IsNullOrEmpty(token) ? Ok(token) : Unauthorized();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserDto userDto)
        {
            try
            {
                var id = await _authService.SignUp(userDto);
                return Ok(id);
            }
            catch(ArgumentException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmUserEmail(string email)
        {
            return Ok(await _authService.ConfirmUserMail(email));
        }
    }
}
