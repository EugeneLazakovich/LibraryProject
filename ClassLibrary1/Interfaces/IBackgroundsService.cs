using System.Threading.Tasks;

namespace Lesson1_BL
{
    public interface IBackgroundsService
    {
        Task<bool> PayPerMonth();
    }
}
