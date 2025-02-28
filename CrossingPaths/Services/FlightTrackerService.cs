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
        public ICollection<(int, int)> Visited { get; } = new List<(int, int)>();

        public void PlotCoordinate((int, int) coordinate)
        {
            //TODO: Implement here
            throw new NotImplementedException();
        }
    }
}
