using Lesson1_BL.Auth;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _genericClientRepository;
        private readonly IGenericRepository<Role> _genericRoleRepository;
        public AuthService(IGenericRepository<User> genericClientRepository, IGenericRepository<Role> genericRoleRepository)
        {
            _genericClientRepository = genericClientRepository;
            _genericRoleRepository = genericRoleRepository;
        }

        public async Task<string> SignIn(string login, string password)
        {
            var user = await _genericClientRepository.GetByPredicate(c => c.Email == login && c.Password == password);

            if (user == null)
            {
                throw new ArgumentException();
            }
            var role = user.RoleId.HasValue ? (await _genericRoleRepository.GetById(user.RoleId.Value)).Name : Roles.Reader;

            return TokenGenerator.GenerateToken(user.Email, role);
        }
    }
}
