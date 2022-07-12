using System.Threading.Tasks;

namespace Lesson1_BL.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> SignIn(string login, string password);
    }
}