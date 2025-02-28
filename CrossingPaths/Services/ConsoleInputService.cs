using CrossingPaths.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingPaths.Services
{
    public class ConsoleInputService : IInputService
    {
        private readonly ILogger<ConsoleInputService> _logger;

        public ConsoleInputService(ILogger<ConsoleInputService> logger)
        {
            _logger = logger;
        }

        public string GetInput()
        {
            var input = Console.ReadLine() ?? string.Empty;
            _logger.LogInformation("User input: {Input}", input);
            return input;
        }
    }
}
