using Lesson1_BL.Auth;
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
        public AuthService(
            IGenericRepository<User> genericClientRepository, 
            IGenericRepository<Role> genericRoleRepository,
            ITokenGenerator tokenGenerator)
        {
            _genericClientRepository = genericClientRepository;
            _genericRoleRepository = genericRoleRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> SignIn(string login, string password)
        {
            var user = await _genericClientRepository.GetByPredicate(c => c.Email == login && c.Password == password);

            if (user == null)
            {
                throw new ArgumentException();
            }
            var role = user.RoleId.HasValue ? (await _genericRoleRepository.GetById(user.RoleId.Value)).Name : Roles.Reader;

            return _tokenGenerator.GenerateToken(user.Email, role);
        }
    }
}
