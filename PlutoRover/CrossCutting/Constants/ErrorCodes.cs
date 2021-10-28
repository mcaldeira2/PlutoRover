
using System;
using PlutoRover.Domain.ValueObjects;

namespace PlutoRover.CrossCutting
{
    // Static class with all the possible errorcodes that can happens on both Vehicle and Grid.
    // I prefer to send an error an let the consumer know what to do instead of throwing an exception inside these class.
    public static class ErrorCodes
    {
        public static readonly Error InvalidInstruction = new Error(100, "Invalid instruction");

        public static readonly Error InvalidInstructions = new Error(101, "Invalid instructions");

        public static readonly Func<Coordinate, Error> UnableToMove = coordinate => new Error(200, $"Vehicle is unable to move to coordinate:\nX: {coordinate.X}\nY: {coordinate.Y}\nVehicle stopped.");

        public static readonly Error NotEnoughElementsInArrayMsg = new Error(201, "Must have at least 2 elements");

        public static readonly Error CoordinateFormatMsg = new Error(202, "Coordinates should have the following format: X,Y");

        public static readonly Error CoordinateOutsideGridBoundsMsg = new Error(203, "The given coordinate is outside grid bounds");

        public static readonly Error SolidObjectAlreadyOnCoordsMsg = new Error(204, "There's already a solid object on that coordinates");
    }
}