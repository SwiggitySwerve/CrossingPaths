using CrossingPaths.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingPaths.Services
{
    public class ConsoleOutputService : IOutputService
    {
        private readonly ILogger<ConsoleOutputService> _logger;

        public ConsoleOutputService(ILogger<ConsoleOutputService> logger)
        {
            _logger = logger;
        }

        public void DisplayResult(string message)
        {
            Console.WriteLine(message);
            _logger.LogInformation("Displayed message: {Message}", message);
        }
    }
}
