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
        public static string GenerateLargeNonIntersectingSpiralPath(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Size must be positive", nameof(size));

            char[] directions = new char[size];

            // We'll create an outward spiral pattern:
            // First go N for 1 step, then E for 1 step, 
            // Then S for 2 steps, W for 2 steps, 
            // Then N for 3 steps, E for 3 steps,
            // And so on, increasing the step count by 1 after each pair of directions

            int currentIndex = 0;
            int segmentLength = 1;
            int segmentsCompleted = 0;

            while (currentIndex < size)
            {
                // Determine current direction based on segment count
                char currentDirection;
                switch (segmentsCompleted % 4)
                {
                    case 0:
                        currentDirection = 'N';
                        break;
                    case 1:
                        currentDirection = 'E';
                        break;
                    case 2:
                        currentDirection = 'S';
                        break;
                    case 3:
                        currentDirection = 'W';
                        break;
                    default:
                        throw new InvalidOperationException("Unexpected segment count");
                }

                // Fill in the current segment with the appropriate direction
                for (int i = 0; i < segmentLength && currentIndex < size; i++)
                {
                    directions[currentIndex++] = currentDirection;
                }

                segmentsCompleted++;

                // Increase segment length every 2 segments (after completing a pair)
                if (segmentsCompleted % 2 == 0)
                {
                    segmentLength++;
                }
            }

            return new string(directions);
        }

        /// <summary>
        /// Generates a zigzag path that never intersects itself.
        /// The zigzag moves east and then north repeatedly, always moving in a new area.
        /// </summary>
        /// <param name="size">The number of steps to generate</param>
        /// <returns>A string of directions (N, E, S, W) forming a zigzag</returns>
        public static string GenerateLargeNonIntersectingZigzagPath(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Size must be positive", nameof(size));

            char[] directions = new char[size];

            // We'll create a zigzag pattern that alternates between:
            // 1. Going East for a long distance
            // 2. Going North for 1 step
            // 3. Going West for a long distance
            // 4. Going North for 1 step
            // This ensures we never cross our path

            int currentIndex = 0;
            int horizontalSteps = Math.Max(1, size / 200); // Adjust this value based on desired zigzag width
            bool goingEast = true;

            while (currentIndex < size)
            {
                // Fill in horizontal segment
                char horizontalDirection = goingEast ? 'E' : 'W';
                for (int i = 0; i < horizontalSteps && currentIndex < size; i++)
                {
                    directions[currentIndex++] = horizontalDirection;
                }

                // Add a single step north if we still have room
                if (currentIndex < size)
                {
                    directions[currentIndex++] = 'N';
                    goingEast = !goingEast; // Switch horizontal direction
                }
            }

            return new string(directions);
        }
    }
}