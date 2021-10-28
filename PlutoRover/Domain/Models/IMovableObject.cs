using System;
using PlutoRover.CrossCutting;
using PlutoRover.Domain.ValueObjects;

namespace PlutoRover.Domain.Models
{
    // Should give mobility to an object
    public interface IMovableObject
    {
        // Moves the object
        Result<Position> Move(string instructions);

        // Checks if object can move to the specified coordinate
        Func<Coordinate, bool> CanMove { get; }
    }
}