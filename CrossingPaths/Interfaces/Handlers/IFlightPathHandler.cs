using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingPaths.Interfaces.Handlers
{
    // Service interfaces
    public interface IFlightPathHandler
    {
        bool IsFlightPlanCrossing(IEnumerable<char> flightPath);
    }
}
