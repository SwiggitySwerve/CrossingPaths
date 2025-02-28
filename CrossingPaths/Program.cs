using CrossingPaths;
using CrossingPaths.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Create host using the shared setup
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices() // Uses the extension method from ServiceSetup
    .ConfigureLogging()  // Uses the extension method from ServiceSetup
    .Build();

var app = host.Services.GetRequiredService<FlightPathApplication>();
await app.RunAsync();