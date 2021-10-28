using PlutoRover.Domain.ValueObjects;

namespace PlutoRover.Domain.Models
{
    // Just another SolidObject that can be used as an obstacle
    public class Rock : SolidObject
    {
        public Rock(Coordinate coordinate) : base(coordinate)
        {
        }
    }
}