using CrossingPaths.Interfaces.Handlers;
using CrossingPaths.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingPaths
{
    public class FlightPathApplication
    {
        private readonly IFlightPathHandler _flightPathHandler;
        private readonly IInputService _inputService;
        private readonly ILogger<FlightPathApplication> _logger;

        public FlightPathApplication(
            IFlightPathHandler flightPathHandler,
            IInputService inputService,
            ILogger<FlightPathApplication> logger)
        {
            _flightPathHandler = flightPathHandler;
            _inputService = inputService;
            _logger = logger;
        }

        public Task RunAsync()
        {
            _logger.LogInformation("Flight Path Application starting");

            var flightPath = _inputService.GetInput();

            var result = _flightPathHandler.IsFlightPlanCrossing(flightPath);
            _logger.LogInformation($"\nCrash Prediction: {result}\n");
            _logger.LogInformation("Flight Path Application completed");

            return Task.CompletedTask;
        }
    }
}
