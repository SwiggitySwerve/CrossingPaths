using CrossingPaths.Handlers;
using CrossingPaths.Infrastructure;
using CrossingPaths.Interfaces.Handlers;
using CrossingPaths.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using System;
using Xunit;

namespace CrossingPaths.Tests
{
    public class FlightPathHandlerTests
    {
        private readonly FlightPathHandler _flightPathHandler;
        private readonly IHost _host;

        public FlightPathHandlerTests()
        {
            // Use the shared service setup class to create a test host
            _host = ServiceSetup.CreateTestHost();

            // Get the handler from the host's service provider
            _flightPathHandler = (FlightPathHandler)_host.Services.GetRequiredService<IFlightPathHandler>();
        }

        [Fact]
        public void GivenExample1_ShouldReturnFalse()
        {
            // Arrange
            EnsureFlightTrackerService();
            var input = "NES";

            // Act
            var output = _flightPathHandler.IsFlightPlanCrossing(input);

            // Assert
            output.ShouldBeFalse();
        }

        [Fact]
        public void GivenExample2_ShouldReturnTrue()
        {
            // Arrange
            EnsureFlightTrackerService();
            var input = "NESWW";

            // Act
            var output = _flightPathHandler.IsFlightPlanCrossing(input);

            // Assert
            output.ShouldBeTrue();
        }

        [Fact]
        public void GivenLongUShapedPath_ShouldReturnFalse()
        {
            // Arrange
            EnsureFlightTrackerService();
            var input = "NNNNNNWSSSS";

            // Act
            var output = _flightPathHandler.IsFlightPlanCrossing(input);

            // Assert
            output.ShouldBeFalse();
        }

        [Fact]
        public void GivenLoop_ShouldReturnTrue()
        {
            // Arrange
            EnsureFlightTrackerService();
            var input = "NNNNNNWSSSSE";

            // Act
            var output = _flightPathHandler.IsFlightPlanCrossing(input);

            // Assert
            output.ShouldBeTrue();
        }

        [Fact]
        public void GivenLoopStartingSouth_ShouldReturnTrue()
        {
            // Arrange
            EnsureFlightTrackerService();
            var input = "SWNNNNNNWSSSSE";

            // Act
            var output = _flightPathHandler.IsFlightPlanCrossing(input);

            // Assert
            output.ShouldBeTrue();
        }

        private void EnsureFlightTrackerService()
        {
            // Get the FlightTrackerService from DI
            var trackerService = _host.Services.GetRequiredService<IFlightTrackerService>();

            // Set it on the handler if needed
            var trackerServiceField = typeof(FlightPathHandler).GetField("_flightTrackerService",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            trackerServiceField?.SetValue(_flightPathHandler, trackerService);
        }
    }
}