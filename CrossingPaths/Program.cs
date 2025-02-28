using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CrossingPaths;
using CrossingPaths.Interfaces.Handlers;
using CrossingPaths.Interfaces.Services;
using CrossingPaths.Services;
using CrossingPaths.Handlers;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // Register services
        services.AddTransient<IFlightPathHandler, FlightPathHandler>();
        services.AddTransient<IFlightDirectionService, FlightDirectionService>();

        services.AddTransient<IInputService, ConsoleInputService>();
        services.AddTransient<IOutputService, ConsoleOutputService>();
        
        services.AddTransient<FlightPathApplication>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

var app = host.Services.GetRequiredService<FlightPathApplication>();
await app.RunAsync();
