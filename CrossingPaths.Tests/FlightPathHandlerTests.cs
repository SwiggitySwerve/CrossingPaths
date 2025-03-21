using CrossingPaths.Handlers;
using CrossingPaths.Infrastructure;
using CrossingPaths.Interfaces.Handlers;
using CrossingPaths.Interfaces.Services;
using CrossingPaths.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace CrossingPaths.Tests
{
    public class FlightPathHandlerTests
    {
        private readonly IHost _host;

        public FlightPathHandlerTests()
        {
            _host = DependencyInjectionExtensions.CreateTestHost();
        }

        private T GetService<T>() => _host.Services.GetRequiredService<T>();

        [Fact]
        public void GivenExample1_ShouldReturnFalse()
        {
            var flightPathHandler = GetService<IFlightPathHandler>();
            var flightTrackerService = GetService<IFlightTrackerService>();

            var input = "NES";
            var expectedVisitedCount = 4;

            var output = flightPathHandler.IsFlightPlanCrossing(input);

            output.ShouldBeFalse();
            flightTrackerService.Visited.Count.ShouldBe(expectedVisitedCount);

            int processedChars = flightTrackerService.Visited.Count - 1;
            processedChars.ShouldBe(input.ToString().Length);
        }

        [Fact]
        public void GivenExample2_ShouldReturnTrue()
        {
            var flightPathHandler = GetService<IFlightPathHandler>();
            var flightTrackerService = GetService<IFlightTrackerService>();

            var input = "NESWW";
            var expectedExitPoint = 4;

            var output = flightPathHandler.IsFlightPlanCrossing(input);

            output.ShouldBeTrue();
            flightTrackerService.Visited.Count.ShouldBe(expectedExitPoint);
            flightTrackerService.Visited.Count.ShouldBeLessThan(input.ToString().Length + 1);
        }

        [Fact]
        public void GivenLongUShapedPath_ShouldReturnFalse()
        {
            var flightPathHandler = GetService<IFlightPathHandler>();
            var flightTrackerService = GetService<IFlightTrackerService>();

            var input = "NNNNNNWSSSS";
            var expectedVisitedCount = 12;

            var output = flightPathHandler.IsFlightPlanCrossing(input);

            output.ShouldBeFalse();
            flightTrackerService.Visited.Count.ShouldBe(expectedVisitedCount);

            int processedChars = flightTrackerService.Visited.Count - 1;
            processedChars.ShouldBe(input.ToString().Length);
        }

        [Fact]
        public void GivenLoop_ShouldReturnTrue()
        {
            var flightPathHandler = GetService<IFlightPathHandler>();
            var flightTrackerService = GetService<IFlightTrackerService>();

            var input = "NNNNNNWSSSSE";

            var output = flightPathHandler.IsFlightPlanCrossing(input);

            output.ShouldBeTrue();
            flightTrackerService.Visited.Count.ShouldBeLessThan(input.ToString().Length + 1);

            int expectedProcessedChars = flightTrackerService.Visited.Count - 1;
            expectedProcessedChars.ShouldBeLessThanOrEqualTo(input.ToString().Length);
        }

        [Fact]
        public void VerifyCrossingPath()
        {
            var flightPathHandler = GetService<IFlightPathHandler>();
            var flightTrackerService = GetService<IFlightTrackerService>();

            var input = "NNEESSWWNNEE";
            var crossingPoint = 8;

            var output = flightPathHandler.IsFlightPlanCrossing(input);

            output.ShouldBeTrue();
            flightTrackerService.Visited.Count.ShouldBe(crossingPoint);

            int processedChars = flightTrackerService.Visited.Count - 1;
            processedChars.ShouldBe(7);
            processedChars.ShouldBeLessThan(input.ToString().Length);
        }

        [Fact]
        public void VerifyEarlyCrossingDetection()
        {
            var flightPathHandler = GetService<IFlightPathHandler>();
            var flightTrackerService = GetService<IFlightTrackerService>();

            var input = "NESWN";

            var output = flightPathHandler.IsFlightPlanCrossing(input);

            output.ShouldBeTrue();
            flightTrackerService.Visited.Count.ShouldBe(4);

            int processedChars = flightTrackerService.Visited.Count - 1;
            processedChars.ShouldBe(3);
            processedChars.ShouldBeLessThan(input.ToString().Length);
        }

        [Fact]
        public void VerifySquarePathCrossing()
        {
            var flightPathHandler = GetService<IFlightPathHandler>();
            var flightTrackerService = GetService<IFlightTrackerService>();

            var input = "NESW";

            var output = flightPathHandler.IsFlightPlanCrossing(input);

            output.ShouldBeTrue();
            flightTrackerService.Visited.Count.ShouldBe(4);

            int processedChars = flightTrackerService.Visited.Count - 1;
            processedChars.ShouldBe(3);
            processedChars.ShouldBeLessThan(input.ToString().Length);
        }

        [Fact]
        public void CharacterProcessingCountValidation()
        {
            {
                var flightPathHandler = GetService<IFlightPathHandler>();
                var flightTrackerService = GetService<IFlightTrackerService>();

                var nonCrossingPath = "NESENNW";
                var nonCrossingResult = flightPathHandler.IsFlightPlanCrossing(nonCrossingPath);
                nonCrossingResult.ShouldBeFalse();
                int nonCrossingProcessed = flightTrackerService.Visited.Count - 1;
                nonCrossingProcessed.ShouldBe(nonCrossingPath.ToString().Length);
            }

            {
                var flightPathHandler = GetService<IFlightPathHandler>();
                var flightTrackerService = GetService<IFlightTrackerService>();

                var crossingPath = "NESWN";
                var crossingResult = flightPathHandler.IsFlightPlanCrossing(crossingPath);
                crossingResult.ShouldBeTrue();

                flightTrackerService.Visited.Count.ShouldBe(10);
            }
        }

        [Fact]
        public void GivenLargeNonIntersectingPath_ShouldHandleEfficientlyAndReturnFalse()
        {
            var flightPathHandler = GetService<IFlightPathHandler>();
            var flightTrackerService = GetService<IFlightTrackerService>();

            var input = TestHelper.GenerateLargeNonIntersectingSpiralPath();

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var output = flightPathHandler.IsFlightPlanCrossing(input);
            stopwatch.Stop();

            output.ShouldBeFalse();
            flightTrackerService.Visited.Count.ShouldBe(input.Length + 1);

            Console.WriteLine($"Processed {input.Length} directions in {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.ElapsedMilliseconds.ShouldBeLessThan(100);
        }

        [Fact]
        public void GivenLargeZigzagPath_ShouldHandleEfficientlyAndReturnFalse()
        {
            var flightPathHandler = GetService<IFlightPathHandler>();
            var flightTrackerService = GetService<IFlightTrackerService>();

            var input = TestHelper.GenerateLargeNonIntersectingZigzagPath();

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var output = flightPathHandler.IsFlightPlanCrossing(input);
            stopwatch.Stop();

            output.ShouldBeFalse();
            flightTrackerService.Visited.Count.ShouldBe(input.Length + 1);

            Console.WriteLine($"Processed {input.Length} directions in {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.ElapsedMilliseconds.ShouldBeLessThan(100);
        }
    }
}