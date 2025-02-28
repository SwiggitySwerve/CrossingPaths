using CrossingPaths.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingPaths.Services
{
    public class FlightDirectionService : IFlightDirectionService
    {
        public FlightDirectionService()
        {

        }

        public (int, int) TravelDirection(char instruction)
        {
            switch (instruction)
            {
                case 'N':
                    return (0, 1);
                case 'S':
                    return (0, -1);
                case 'E':
                    return (1, 0);
                case 'W':
                    return (-1, 0);
                default:
                    return (0, 0);
            }
        }
    }
}
