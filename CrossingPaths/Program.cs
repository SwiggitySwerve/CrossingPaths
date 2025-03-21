using CrossingPaths;
using CrossingPaths.Handlers;
using CrossingPaths.Interfaces.Handlers;
using CrossingPaths.Interfaces.Services;
using CrossingPaths.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
.ConfigureServices((hostContext, services) =>
{
    // Register core services
    services.AddScoped<IFlightPathHandler, FlightPathHandler>();
    services.AddScoped<IFlightDirectionService, FlightDirectionService>();
    services.AddScoped<IFlightTrackerService, FlightTrackerService>();


    // Register I/O services  
    services.AddTransient<IInputService, ConsoleInputService>();
    services.AddTransient<IOutputService, ConsoleOutputService>();

    // Register your main application  
    services.AddTransient<FlightPathApplication>();
})
.Build();

var app = host.Services.GetRequiredService<FlightPathApplication>();
await app.RunAsync();