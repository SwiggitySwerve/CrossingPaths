using CrossingPaths;
using CrossingPaths.Handlers;
using CrossingPaths.Infrastructure;
using CrossingPaths.Interfaces.Handlers;
using CrossingPaths.Interfaces.Services;
using CrossingPaths.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//Use DependencyInjectionExtensions
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices() 
    .ConfigureLogging()
    .Build();

var app = host.Services.GetRequiredService<FlightPathApplication>();
await app.RunAsync();