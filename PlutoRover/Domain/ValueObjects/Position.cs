using PlutoRover.CrossCutting;
using PlutoRover.Domain.Enums;

namespace PlutoRover.Domain.ValueObjects
{
    public record Position(Direction Direction, Coordinate Coordinate);
}
