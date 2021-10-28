using PlutoRover.Domain.ValueObjects;

namespace PlutoRover.Domain.Models
{
    // Just another alternative to a solid object
    public class AlienSpaceship : SolidObject
    {
        public AlienSpaceship(Coordinate coordinate) : base(coordinate)
        {
        }
    }
}
