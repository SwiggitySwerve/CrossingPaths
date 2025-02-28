using CrossingPaths.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingPaths.Services
{
    public class FlightTrackerService : IFlightTrackerService
    {
        public List<(int, int)> Visited { get; } = new();

        public void PlotCoordinate((int, int) coordinate)
        {
            Visited.Add(coordinate);
        }
    }
}
