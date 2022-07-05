using Lesson1.Controllers;
using Lesson1_BL;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson1
{
    public class BackgroundController
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IBackgroundsService _backgroundService;

        public BackgroundController(IBackgroundsService backgroundService, ILogger<ClientsController> logger)
        {
            _backgroundService = backgroundService;
            _logger = logger;
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
