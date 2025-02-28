using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingPaths.Interfaces.Services
{
    public interface IFlightDirectionService
    {
        public (int, int) TravelDirection(char instruction);
    }
}
