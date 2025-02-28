using CrossingPaths.Handlers;
using CrossingPaths.Interfaces.Handlers;
using CrossingPaths.Interfaces.Services;
using CrossingPaths.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CrossingPaths.Infrastructure
{
    /// <summary>
    /// A reusable setup class for configuring dependency injection
    /// that can be used by both tests and the main application.
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Configures the host builder with all required services.
        /// </summary>
        /// <param name="hostBuilder">The host builder to configure</param>
        /// <returns>The configured host builder</returns>
        public static IHostBuilder ConfigureServices(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                // Register common services
                RegisterCommonServices(services);

                // Register application-specific services
                services.AddTransient<FlightPathApplication>();
            });
        }

        /// <summary>
        /// Configures the service collection with all required services.
        /// This can be used directly in test setup.
        /// </summary>
        /// <param name="services">The service collection to configure</param>
        /// <returns>The configured service collection</returns>
        public static IServiceCollection RegisterCommonServices(this IServiceCollection services)
        {
            // Register core services
            services.AddScoped<IFlightPathHandler, FlightPathHandler>();
            services.AddScoped<IFlightDirectionService, FlightDirectionService>();
            services.AddScoped<IFlightTrackerService, FlightTrackerService>();

            // Register I/O services
            services.AddTransient<IInputService, ConsoleInputService>();
            services.AddTransient<IOutputService, ConsoleOutputService>();

            return services;
        }

        /// <summary>
        /// Configures logging for both the application and tests.
        /// </summary>
        /// <param name="hostBuilder">The host builder to configure</param>
        /// <returns>The configured host builder</returns>
        public static IHostBuilder ConfigureLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
        }

        /// <summary>
        /// A helper method to create a configured host for tests.
        /// </summary>
        /// <returns>A fully configured IHost instance ready for tests</returns>
        public static IHost CreateTestHost()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices()
                .ConfigureLogging()
                .Build();
        }
    }
}