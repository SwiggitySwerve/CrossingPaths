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
        public List<(int, int)> Visited { get; }

        public void PlotCoordinate((int, int) coordinate)
        {
            throw new NotImplementedException();
        }
    }
}
