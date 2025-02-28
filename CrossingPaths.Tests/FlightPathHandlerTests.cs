using CrossingPaths.Handlers;
using Shouldly;

namespace CrossingPaths.Tests;

public class FlightPathHandlerTests
{
    private readonly FlightPathHandler _flightPathHandler = new();

    [Fact]
    public void GivenExample1_ShouldReturnFalse()
    {
        // Arrange
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
        var input = "SWNNNNNNWSSSSE";

        // Act
        var output = _flightPathHandler.IsFlightPlanCrossing(input);

        // Assert
        output.ShouldBeTrue();
    }
}