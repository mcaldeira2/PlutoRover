using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PlutoRover.Domain.Enums;
using PlutoRover.CrossCutting;
using PlutoRover.Domain.ValueObjects;
using System.Linq;

namespace PlutoRover.Domain.Models
{
    // Vehicle class that can be used as our rover
    public class Vehicle : IVehicle
    {
        public Vehicle(Func<Coordinate, bool> canMoveFun) => (Direction, Coordinate, CanMove) = (Direction.North, new Coordinate(0, 0), canMoveFun);

        public Vehicle(Direction direction, Coordinate coordinate, Func<Coordinate, bool> canMoveFun) =>
            (Direction, Coordinate, CanMove) = (direction, coordinate, canMoveFun);

        public const char ForwardInput = 'f';

        public const char BackwardsInput = 'b';

        public const char LeftInput = 'l';

        public const char RightInput = 'r';

        public Position Position { get => new Position(Direction, Coordinate); }

        public Func<Coordinate, bool> CanMove { get; private set; }

        // Instructions to move the vehicle
        public IReadOnlyCollection<char> Instructions => new ReadOnlyCollection<char>(new[] { ForwardInput, BackwardsInput, LeftInput, RightInput });

        public Direction Direction { get; private set; }

        public Coordinate Coordinate { get; private set; }

        public List<char> History { get; private set; } = new List<char>();

        // Moves vehicle 90degrees right
        public Result<Position> TurnRight()
        {
            Direction = Direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new ArgumentOutOfRangeException()
            };

            History.Add(RightInput);

            return new Result<Position>(Position);
        }

        // Moves vehicle 90degrees left
        public Result<Position> TurnLeft()
        {
            Direction = Direction switch
            {
                Direction.North => Direction.West,
                Direction.East => Direction.North,
                Direction.South => Direction.East,
                Direction.West => Direction.South,
                _ => throw new ArgumentOutOfRangeException()
            };

            History.Add(LeftInput);

            return new Result<Position>(Position);
        }

        // Tries to move vehicle a cell forward
        public Result<Position> Forwards()
        {
            var coordinate = Direction switch
            {
                Direction.North => Coordinate with { Y = Coordinate.Y + 1 },
                Direction.East => Coordinate with { X = Coordinate.X + 1 },
                Direction.South => Coordinate with { Y = Coordinate.Y - 1 },
                Direction.West => Coordinate with { X = Coordinate.X - 1 },
                _ => Coordinate,
            };

            if (!CanMove(coordinate))
            {
                return new Result<Position>(ErrorCodes.UnableToMove(coordinate));
            }

            Coordinate = coordinate;

            History.Add(ForwardInput);

            return new Result<Position>(Position);
        }

        // Tries to move vehicle a cell backwards
        public Result<Position> Backwards()
        {
            var coordinate = Direction switch
            {
                Direction.North => Coordinate with { Y = Coordinate.Y - 1 },
                Direction.East => Coordinate with { X = Coordinate.X - 1 },
                Direction.South => Coordinate with { Y = Coordinate.Y + 1 },
                Direction.West => Coordinate with { X = Coordinate.X + 1 },
                _ => Coordinate,
            };

            if (!CanMove(coordinate))
            {
                return new Result<Position>(ErrorCodes.UnableToMove(coordinate));
            }

            Coordinate = coordinate;

            History.Add(BackwardsInput);

            return new Result<Position>(Position);
        }

        // Reports vehicle current coordinates
        public string ReportCoordinates() => $"Vehicle current position:\nX: {Coordinate.X}\nY: {Coordinate.Y}\nFacing: {Direction.ToString()}";

        // Reports all the instructions accepted by the vehicle
        public string ReportHistory() => History.Count != 0 ? $"Vehicle history: {string.Join(',', History)}" : "No history found";

        // Tries to move the vehicle using a sequence of instructions if all are acceptable
        public Result<Position> Move(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions))
            {
                return new Result<Position>(Position);
            }

            // Only moves if all instructions are ok
            if (instructions.Any(instruction => !Instructions.Contains(instruction)))
            {
                return new Result<Position>(ErrorCodes.InvalidInstructions);
            }

            Result<Position> result = null;

            foreach (var instruction in instructions)
            {
                result = ProcessInstruction(instruction);
                if (result.HasError)
                {
                    break;
                }
            }

            return result;
        }

        // Process an instruction
        private Result<Position> ProcessInstruction(char instruction)
        {
            switch (instruction)
            {
                case ForwardInput:
                    return Forwards();
                case BackwardsInput:
                    return Backwards();
                case LeftInput:
                    return TurnLeft();
                case RightInput:
                    return TurnRight();
                default:
                    return new Result<Position>(ErrorCodes.InvalidInstruction);
            }
        }
    }
}