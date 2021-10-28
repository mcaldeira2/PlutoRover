using System;
using System.Collections.Generic;
using PlutoRover.CrossCutting;
using PlutoRover.Domain.ValueObjects;

namespace PlutoRover.Domain.Models
{
    // Can be used to our Rover
    public interface IVehicle : ISolidObject, IMovableObject
    {
        // Instructions to move the vehicle
        IReadOnlyCollection<char> Instructions { get; }

        // Moves vehicle 90degrees left
        Result<Position> TurnLeft();

        // Moves vehicle 90degrees right
        Result<Position> TurnRight();

        // Tries to move vehicle a cell forward
        Result<Position> Forwards();

        // Tries to move vehicle a cell backwards
        Result<Position> Backwards();

        // Reports vehicle current coordinates
        string ReportCoordinates();

        // Reports all the instructions accepted by the vehicle
        string ReportHistory();
    }
}













