using System;
using System.Text;

namespace CrossingPaths.Tests
{
    public static class TestHelper
    {
        public static int ITERATIONS = 100000;

        /// <summary>
        /// Generates a spiral path that never intersects itself.
        /// The spiral grows outward, with each edge of the spiral increasing in length.
        /// </summary>
        /// <param name="size">The approximate number of steps to generate</param>
        /// <returns>A string of directions (N, E, S, W) forming a spiral</returns>
        public static string GenerateLargeNonIntersectingSpiralPath()
        {
            StringBuilder spiral = new StringBuilder(ITERATIONS);
            int segment = 0;  // Each loop iteration defines one segment  

            // Build segments until we have at least ITERATIONS steps.  
            while (spiral.Length < ITERATIONS)
            {
                // Determine the number of steps for this segment (each segment grows by one step each time).  
                int steps = segment + 1;
                if (segment % 2 == 0)
                {
                    // Even segments: add 'N' repeated steps then 'E' repeated steps.  
                    spiral.Append(new string('N', steps));
                    spiral.Append(new string('E', steps));
                }
                else
                {
                    // Odd segments: add 'S' repeated steps then 'W' repeated steps.  
                    spiral.Append(new string('S', steps));
                    spiral.Append(new string('W', steps));
                }
                segment++;
            }

            // If the spiral exceeds the specified ITERATIONS, truncate it.  
            if (spiral.Length > ITERATIONS)
            {
                spiral.Length = ITERATIONS;
            }

            return spiral.ToString();
        }

        /// <summary>  
        /// Generates a zigzag path that never intersects itself.  
        /// This version uses a minimal repeating pattern: "NE" repeated over and over.  
        /// For example, for ITERATIONS = 8, it produces "NENENENE".  
        /// </summary>  
        /// <returns>A string of directions (N, E) forming a zigzag</returns>  
        public static string GenerateLargeNonIntersectingZigzagPath()
        {
            int size = ITERATIONS;
            char[] directions = new char[size];

            // Simply alternate 'N' and 'E'  
            for (int i = 0; i < size; i++)
            {
                directions[i] = (i % 2 == 0) ? 'N' : 'E';
            }

            return new string(directions);
        }
    }
}