using Lesson1_BL.Auth;
using Lesson1_BL.DTOs;
using Lesson1_BL.Services.HashService;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using System;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _genericClientRepository;
        private readonly IGenericRepository<Role> _genericRoleRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IHashService _hashService;
        public AuthService(
            IGenericRepository<User> genericClientRepository, 
            IGenericRepository<Role> genericRoleRepository,
            ITokenGenerator tokenGenerator,
            IHashService hashService)
        {
            _genericClientRepository = genericClientRepository;
            _genericRoleRepository = genericRoleRepository;
            _tokenGenerator = tokenGenerator;
            _hashService = hashService;
        }

        public async Task<string> SignIn(string login, string password)
        {
            var user = await _genericClientRepository
                .GetByPredicate(c => c.Email == login && c.Password == _hashService.HashString(password));

            if (user == null)
            {
                throw new ArgumentException();
            }
            var role = user.RoleId.HasValue ? (await _genericRoleRepository.GetById(user.RoleId.Value)).Name : Roles.Reader;

            return _tokenGenerator.GenerateToken(user.Email, role);
        }

        public async Task<Guid> SignUp(UserDto user)
        {
            user.Password = _hashService.HashString(user.Password);

            return await _genericClientRepository.Add(MapUserDtoToUser(user));
        }

        private User MapUserDtoToUser(UserDto userDto)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                BirthDate = userDto.BirthDate,
                Email = userDto.Email,
                Password = userDto.Password,
                IsBlocked = false,
                Amount = 0
            };
        }
    }
}
