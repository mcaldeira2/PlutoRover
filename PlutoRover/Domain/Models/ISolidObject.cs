using PlutoRover.Domain.ValueObjects;

namespace PlutoRover.Domain.Models
{
    // Represents an obstacle on the grid
    public interface ISolidObject
    {
        Coordinate Coordinate { get; }
    }
}