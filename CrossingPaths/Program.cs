using CrossingPaths;

Console.Write("Input Flight Path: ");
var input = Console.ReadLine() ?? string.Empty;

var output = new FlightPathHandler().IsFlightPlanCrossing(input);

Console.WriteLine($"Crash Prediction: {output}");