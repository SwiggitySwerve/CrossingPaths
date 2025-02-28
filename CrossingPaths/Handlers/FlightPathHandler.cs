using CrossingPaths.Interfaces.Handlers;
using CrossingPaths.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingPaths.Handlers
{
    public class FlightPathHandler : IFlightPathHandler
    {
        private readonly IFlightDirectionService _flightDirectionService;
        private readonly IFlightTrackerService _flightTrackerService;
        private readonly ILogger<FlightPathHandler> _logger;

        private (int, int) Location = (0, 0);

        public FlightPathHandler(IFlightDirectionService flightDirectionService,
            ILogger<FlightPathHandler> logger)
        {
            _flightDirectionService = flightDirectionService;
            _logger = logger;
        }

        public bool IsFlightPlanCrossing(IEnumerable<char> flightPath)
        {
            bool result = default;

            _logger.LogInformation("Analyzing flight path: {FlightPath}", flightPath);
            _flightTrackerService.PlotCoordinate(Location);

            foreach (char instruction in flightPath)
            {
                var direction = _flightDirectionService.TravelDirection(instruction);
                Location.Item1 += direction.Item1;
                Location.Item2 += direction.Item2;
                _flightTrackerService.PlotCoordinate(Location);
            }

            _logger.LogInformation("Flight path analysis result: {Result}", result);
            return result;
        }
    }
}
