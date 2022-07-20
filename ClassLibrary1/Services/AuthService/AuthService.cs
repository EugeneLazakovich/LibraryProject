using Lesson1_BL.Auth;
using Lesson1_BL.DTOs;
using Lesson1_BL.Services.EncryptionService;
using Lesson1_BL.Services.HashService;
using Lesson1_BL.Services.SMTPService;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lesson1_BL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _genericClientRepository;
        private readonly IGenericRepository<Role> _genericRoleRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IHashService _hashService;
        private readonly ISendingBlueSmtpService _sendingBlueSmtpService;
        private readonly IEncryptionService _encryptionService;
        public AuthService(
            IEncryptionService encryptionService,
            IGenericRepository<User> genericClientRepository, 
            IGenericRepository<Role> genericRoleRepository,
            ITokenGenerator tokenGenerator,
            IHashService hashService,
            ISendingBlueSmtpService sendingBlueSmtpService)
        {
            _encryptionService = encryptionService;
            _genericClientRepository = genericClientRepository;
            _genericRoleRepository = genericRoleRepository;
            _tokenGenerator = tokenGenerator;
            _hashService = hashService;
            _sendingBlueSmtpService = sendingBlueSmtpService;
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
            var userDB = await _genericClientRepository.GetByPredicate(x => x.Email == user.Email);
            if(userDB != null)
            {
                throw new ArgumentException("User with this email has already been created!");
            }
            if (!IsCorrectEmail(user.Email))
            {
                throw new ArgumentException("Email is not correct!");
            }
            user.Password = _hashService.HashString(user.Password);

            var response = await _genericClientRepository.Add(MapUserDtoToUser(user));
            await _sendingBlueSmtpService.SendMail(
                new MailInfo 
                {
                    ClientName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    Subject = "Email confiramtion",
                    Body = "https://localhost:5001/users/confirm?email=" + GenerateConfirmationString(user.Email)
                });

            return response;
        }        

        public async Task<bool> ConfirmUserMail(string encryptedEmail)
        {
            encryptedEmail = encryptedEmail.Replace(' ', '+');
            var userEmail = _encryptionService.DecryptString(encryptedEmail);
            var user = await _genericClientRepository.GetByPredicate(x => x.Email == userEmail);
            if(user != null)
            {
                user.IsConfirmed = true;
                await _genericClientRepository.Update(user);
            }

            return user != null;
        }

        private bool IsCorrectEmail(string email)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
                +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private string GenerateConfirmationString(string email)
        {
            return _encryptionService.EncryptString(email);
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
                Amount = 0,
                IsConfirmed = false
            };
        }
    }
}
