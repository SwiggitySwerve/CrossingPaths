using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Xunit;
using CrossingPaths;
using CrossingPaths.Handlers;
using CrossingPaths.Interfaces.Handlers;
using CrossingPaths.Interfaces.Services;
using CrossingPaths.Services;
using CrossingPaths.Infrastructure;

namespace CrossingPaths.Tests
{
    public class DIRegistrationsTests
    {
        private readonly IHost _host;


        public DIRegistrationsTests()
        {
            // Create a host with the same DI registrations from DependencyInjectionExtensions as the main program
            _host = Host.CreateDefaultBuilder()
                        .ConfigureServices()
                        .ConfigureLogging()
                        .Build();
        }

        // Helper method to resolve services.  
        private T GetService<T>()
        {
            return _host.Services.GetRequiredService<T>();
        }

        [Fact]
        public void IFlightPathHandler_ShouldBeRegistered()
        {
            var service = GetService<IFlightPathHandler>();
            service.ShouldNotBeNull();
            service.ShouldBeOfType<FlightPathHandler>();
        }

        [Fact]
        public void IFlightDirectionService_ShouldBeRegistered()
        {
            var service = GetService<IFlightDirectionService>();
            service.ShouldNotBeNull();
            service.ShouldBeOfType<FlightDirectionService>();
        }

        [Fact]
        public void IFlightTrackerService_ShouldBeRegistered()
        {
            var service = GetService<IFlightTrackerService>();
            service.ShouldNotBeNull();
            service.ShouldBeOfType<FlightTrackerService>();
        }

        [Fact]
        public void IInputService_ShouldBeRegistered()
        {
            var service = GetService<IInputService>();
            service.ShouldNotBeNull();
            service.ShouldBeOfType<ConsoleInputService>();
        }

        [Fact]
        public void FlightPathApplication_ShouldBeRegistered()
        {
            var app = GetService<FlightPathApplication>();
            app.ShouldNotBeNull();
        }

        [Fact]
        public void AllExpectedServices_AreRegistered()
        {
            // This test checks all expected services at once.  
            GetService<IFlightPathHandler>().ShouldNotBeNull();
            GetService<IFlightDirectionService>().ShouldNotBeNull();
            GetService<IFlightTrackerService>().ShouldNotBeNull();
            GetService<IInputService>().ShouldNotBeNull();
            GetService<FlightPathApplication>().ShouldNotBeNull();
        }
    }
}