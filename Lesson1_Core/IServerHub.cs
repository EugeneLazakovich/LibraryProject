using System.Threading.Tasks;

namespace Lesson1_Core
{
    public interface IServerHub
    {
        Task SendMessage(string message);
        Task<bool> SignIn(string username, string password);
    }
}