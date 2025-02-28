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
        public ICollection<(int, int)> Visited { get; } = new HashSet<(int, int)>();

        public void PlotCoordinate((int, int) coordinate)
        {
            Visited.Add(coordinate);
        }
    }
}
