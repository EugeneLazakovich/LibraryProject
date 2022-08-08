using AutoFixture;
using Lesson1_BL.Auth;
using Lesson1_BL.DTOs;
using Lesson1_BL.Services.AuthService;
using Lesson1_BL.Services.EncryptionService;
using Lesson1_BL.Services.HashService;
using Lesson1_BL.Services.SMTPService;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Lesson1_BL.Tests
{
    public class AuthServiceTests
    {
        private Mock<IGenericRepository<User>> _genericUserRepositoryMock;
        private Mock<IGenericRepository<Role>> _genericRoleRepositoryMock;
        private Mock<ITokenGenerator> _tokenGeneratorMock;
        private Mock<IHashService> _hashServiceMock;
        private Mock<ISendingBlueSmtpService> _sendingBlueSmtpServiceMock;
        private Mock<IEncryptionService> _encryptionServiceMock;
        private AuthService _authService;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _genericUserRepositoryMock = new Mock<IGenericRepository<User>>();
            _genericRoleRepositoryMock = new Mock<IGenericRepository<Role>>();
            _tokenGeneratorMock = new Mock<ITokenGenerator>();
            _hashServiceMock = new Mock<IHashService>();
            _sendingBlueSmtpServiceMock = new Mock<ISendingBlueSmtpService>();
            _encryptionServiceMock = new Mock<IEncryptionService>();
            _authService = new AuthService(
                _encryptionServiceMock.Object,
                _genericUserRepositoryMock.Object, 
                _genericRoleRepositoryMock.Object,
                _tokenGeneratorMock.Object,
                _hashServiceMock.Object,
                _sendingBlueSmtpServiceMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task SignIn_WhenUserWithoutRole_ShouldLoginUserWithReader()
        {            
            var login = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var hashPassword = _fixture.Create<string>();
            var userInDb = _fixture.Create<User>();
            var expectedToken = _fixture.Create<string>();
            userInDb.Email = login;
            userInDb.Role = null;
            userInDb.RoleId = null;

            _hashServiceMock
                .Setup(x => x.HashString(password))
                .Returns(hashPassword)
                .Verifiable();

            _genericUserRepositoryMock
                .Setup(x => x.GetByPredicate(c => c.Email == login && c.Password == hashPassword))
                .ReturnsAsync(userInDb)
                .Verifiable();
            _tokenGeneratorMock
                .Setup(x => x.GenerateToken(login, Roles.Reader))
                .Returns(expectedToken)
                .Verifiable();

            var actualToken = await _authService.SignIn(login, password);
            _hashServiceMock.Verify();
            _genericUserRepositoryMock.Verify();
            _genericRoleRepositoryMock.Verify(
                x => x.GetById(It.IsAny<Guid>()), Times.Never);
            _tokenGeneratorMock.Verify();

            Assert.AreEqual(expectedToken, actualToken);
        }

        [Test]
        public async Task SignIn_WhenUserWithRole_ShouldLoginUserWithRole()
        {
            var login = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var hashPassword = _fixture.Create<string>();
            var userInDb = _fixture.Create<User>();
            var expectedToken = _fixture.Create<string>();
            var role = _fixture.Create<Role>();
            userInDb.Email = login;

            _hashServiceMock
                .Setup(x => x.HashString(password))
                .Returns(hashPassword)
                .Verifiable();
            _genericUserRepositoryMock
                .Setup(x => x.GetByPredicate(c => c.Email == login && c.Password == hashPassword))
                .ReturnsAsync(userInDb)
                .Verifiable();
            _genericRoleRepositoryMock
                .Setup(x => x.GetById(userInDb.RoleId.Value))
                .ReturnsAsync(role)
                .Verifiable();
            _tokenGeneratorMock
                .Setup(x => x.GenerateToken(login, role.Name))
                .Returns(expectedToken)
                .Verifiable();

            var actualToken = await _authService.SignIn(login, password);
            _hashServiceMock.Verify();
            _genericUserRepositoryMock.Verify();
            _genericRoleRepositoryMock.Verify();
            _tokenGeneratorMock.Verify();

            Assert.AreEqual(expectedToken, actualToken);
        }

        [Test]
        public async Task SignIn_WhenNoUserInDB_ShouldThrowArgumentException()
        {
            var login = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var hashPassword = _fixture.Create<string>();

            _hashServiceMock
                .Setup(x => x.HashString(password))
                .Returns(hashPassword)
                .Verifiable();
            _genericUserRepositoryMock
                .Setup(x => x.GetByPredicate(c => c.Email == login && c.Password == hashPassword))
                .Verifiable();

            Assert.ThrowsAsync<ArgumentException>(async () => await _authService.SignIn(login, password));
            _hashServiceMock.Verify();
            _genericUserRepositoryMock.Verify();
            _genericRoleRepositoryMock
                .Verify(x => x.GetById(It.IsAny<Guid>()), Times.Never);
            _tokenGeneratorMock
                .Verify(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<string>()), Times.Never);            
        }

        [Test]
        public async Task SignUp_WhenCalled_ShouldRegisterUser()
        {
            _fixture.Customize<UserDto>(c => c
                .With(x => x.Email, _fixture.Create<MailAddress>().Address));
            var userDto = _fixture.Create<UserDto>();
            var expectedGuid = _fixture.Create<Guid>();
            var hashPassword = _fixture.Create<string>();
            var confirmationString = _fixture.Create<string>();
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                BirthDate = userDto.BirthDate,
                Amount = userDto.Amount,
                Email = userDto.Email,
                Password = userDto.Password
            };
            var mailInfo = new MailInfo
            {
                ClientName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Subject = "Email confirmation",
                Body = "https://localhost:5001/users/confirm?email=" + confirmationString
            };            

            _hashServiceMock
                .Setup(x => x.HashString(userDto.Password))
                .Returns(hashPassword)
                .Verifiable();
            _genericUserRepositoryMock
                .Setup(x => x.Add(It.Is<User>(
                    x => x.Id == user.Id 
                    && x.Email == user.Email)))
                .ReturnsAsync(expectedGuid)
                .Verifiable();
            _encryptionServiceMock
                .Setup(x => x.EncryptString(userDto.Email))
                .Returns(confirmationString)
                .Verifiable();
            _sendingBlueSmtpServiceMock
                .Setup(x => x.SendMail(It.Is<MailInfo>(
                    x => x.Email == mailInfo.Email 
                    && x.ClientName == mailInfo.ClientName
                    && x.Subject == mailInfo.Subject
                    && x.Body == mailInfo.Body)))
                .Verifiable();

            var actualGuid = await _authService.SignUp(userDto);

            _hashServiceMock.Verify();
            _genericUserRepositoryMock.Verify();
            _encryptionServiceMock.Verify();
            _sendingBlueSmtpServiceMock.Verify();

            Assert.AreEqual(expectedGuid, actualGuid);
        }

        [Test]
        public async Task ConfirmUserMail_WhenUserExist_ShouldConfirmEmail()
        {
            _fixture.Customize<User>(c => c
                .With(x => x.Email, _fixture.Create<MailAddress>().Address));
            var encryptedEmail = _fixture.Create<string>();
            encryptedEmail += "sdf adf asd";
            var emailWithoutSpaces = encryptedEmail.Replace(' ', '+');
            var decryptedEmail = _fixture.Create<string>();
            var user = _fixture.Create<User>();
            user.IsConfirmed = false;

            _encryptionServiceMock
                .Setup(x => x.DecryptString(emailWithoutSpaces))
                .Returns(decryptedEmail)
                .Verifiable();
            _genericUserRepositoryMock
                .Setup(x =>
                    x.GetByPredicate(x =>
                        x.Email == decryptedEmail))
                .ReturnsAsync(user)
                .Verifiable();
            _genericUserRepositoryMock
                .Setup(
                    x => x.Update(
                        It.Is<User>(x =>
                            x.Id == user.Id
                            && x.IsConfirmed == true)))
                .Verifiable();

            var actual = await _authService.ConfirmUserMail(encryptedEmail);

            Assert.True(actual);
            _encryptionServiceMock.Verify();
            _genericUserRepositoryMock.Verify();
        }

        [Test]
        public async Task ConfirmUserMail_WhenUserDoesntExist_ShouldThrowArgumentException()
        {
            var encryptedEmail = _fixture.Create<string>();
            encryptedEmail += "sdf adf asd";
            var emailWithoutSpaces = encryptedEmail.Replace(' ', '+');
            var decryptedEmail = _fixture.Create<string>();

            _encryptionServiceMock
                .Setup(x => x.DecryptString(emailWithoutSpaces))
                .Returns(decryptedEmail)
                .Verifiable();
            _genericUserRepositoryMock
                .Setup(x =>
                    x.GetByPredicate(x =>
                        x.Email == decryptedEmail))
                .Verifiable();

            var actual = await _authService.ConfirmUserMail(encryptedEmail);

            Assert.False(actual);
            _encryptionServiceMock.Verify();
            _genericUserRepositoryMock.Verify();
            _genericUserRepositoryMock
                .Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }
    }
}
