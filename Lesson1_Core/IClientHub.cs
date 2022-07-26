using System.Threading.Tasks;

namespace Lesson1_Core
{
    public interface IClientHub
    {
        Task ReceiveMessage(string user, string message);
    }
}