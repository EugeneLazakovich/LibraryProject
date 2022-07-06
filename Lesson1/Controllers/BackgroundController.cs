using Lesson1_BL;
using System.Threading;

namespace Lesson1.Controllers
{
    public class BackgroundController
    {
        private readonly IBackgroundsService _backgroundService;

        public BackgroundController(IBackgroundsService backgroundService)
        {
            _backgroundService = backgroundService;
        }
        public void StartListning()
        {
            while (true)
            {
                _backgroundService.PayPerMonth();
                Thread.Sleep(86400000);
            }
        }
    }
}
