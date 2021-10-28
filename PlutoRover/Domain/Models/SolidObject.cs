using PlutoRover.Domain.ValueObjects;

namespace PlutoRover.Domain.Models
{
    // Represents a solid object that can be used as an obstacle
    public class SolidObject : ISolidObject
    {
        public Coordinate Coordinate { get; set; }

        public SolidObject(Coordinate coordinate) => Coordinate = coordinate;
    }
}