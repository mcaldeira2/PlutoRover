using PlutoRover.Domain.Enums;
using PlutoRover.Domain.Models;
using PlutoRover.Domain.ValueObjects;
using Xunit;

namespace PlutoRoverTests.Models
{
    public class VehicleTests
    {
        [Theory]
        [InlineData(Direction.North, 0, 1)]
        [InlineData(Direction.South, 0, -1)]
        [InlineData(Direction.West, -1, 0)]
        [InlineData(Direction.East, 1, 0)]

        public void Move_WhenVehicleCanAlwaysMoveIsPointingTowardsADirectionAndMovesForward_NewCoordinatesShouldBeRight(Direction direction, int x, int y)
        {
            //Arrange
            var vehicle = new Vehicle(direction, new Coordinate(0, 0), _ => true);

            //Act
            var result = vehicle.Move(Vehicle.ForwardInput.ToString());

            //Assert
            Assert.Equal(x, result.Data.Coordinate.X);
            Assert.Equal(y, result.Data.Coordinate.Y);
        }

        [Theory]
        [InlineData(Direction.North, 0, -1)]
        [InlineData(Direction.South, 0, 1)]
        [InlineData(Direction.West, 1, 0)]
        [InlineData(Direction.East, -1, 0)]

        public void Move_WhenVehicleCanAlwaysMoveIsPointingTowardsADirectionAndMovesBackwards_NewCoordinatesShouldBeRight(Direction direction, int x, int y)
        {
            //Arrange
            var vehicle = new Vehicle(direction, new Coordinate(0, 0), _ => true);

            //Act
            var result = vehicle.Move(Vehicle.BackwardsInput.ToString());

            //Assert
            Assert.Equal(x, result.Data.Coordinate.X);
            Assert.Equal(y, result.Data.Coordinate.Y);
        }

        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        [InlineData(Direction.East, Direction.South)]

        public void Move_WhenVehicleCanAlwaysMoveIsPointingTowardsADirectionAndTurnsRight_NewDirectionShouldBeRight(Direction direction, Direction expected)
        {
            //Arrange
            var vehicle = new Vehicle(direction, new Coordinate(0, 0), _ => true);

            //Act
            var result = vehicle.Move(Vehicle.RightInput.ToString());

            //Assert
            Assert.Equal(expected, result.Data.Direction);
        }

        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.West, Direction.South)]
        [InlineData(Direction.East, Direction.North)]

        public void Move_WhenVehicleCanAlwaysMoveIsPointingTowardsADirectionAndTurnsLeft_NewDirectionShouldBeRight(Direction direction, Direction expected)
        {
            //Arrange
            var vehicle = new Vehicle(direction, new Coordinate(0, 0), _ => true);

            //Act
            var result = vehicle.Move(Vehicle.LeftInput.ToString());

            //Assert
            Assert.Equal(expected, result.Data.Direction);
        }

        [Fact]
        public void Move_WhenVehicleCantMove_ErrorIsReceived()
        {
            // Arrange
            var startingPoint = new Coordinate(0, 0);
            var vehicle = new Vehicle(Direction.North, startingPoint, _ => false);

            // Act
            var newCoordinates = vehicle.Move(Vehicle.BackwardsInput.ToString());

            // Assert
            Assert.True(newCoordinates.HasError);
        }

        [Fact]
        public void Move_WhenInstructionIsNull_CoordinatesShouldRemainTheSame()
        {
            // Arrange
            var startingPoint = new Coordinate(0, 0);
            var vehicle = new Vehicle(Direction.North, startingPoint, _ => true);

            // Act
            var newCoordinates = vehicle.Move(null);

            // Assert
            Assert.Equal(startingPoint, newCoordinates.Data.Coordinate);
        }

        [Fact]
        public void Move_WhenInstructionIsWhiteSpace_CoordinatesShouldRemainTheSame()
        {
            // Arrange
            var startingPoint = new Coordinate(0, 0);
            var vehicle = new Vehicle(Direction.North, startingPoint, _ => true);

            // Act
            var newCoordinates = vehicle.Move(" ");

            // Assert
            Assert.Equal(startingPoint, newCoordinates.Data.Coordinate);
        }

        [Fact]
        public void Move_WhenInstructionIsInvalid_ErrorIsReceived()
        {
            // Arrange
            var vehicle = new Vehicle(_ => true);

            // Act
            var newCoordinates = vehicle.Move("«");

            // Assert
            Assert.True(newCoordinates.HasError);
        }
    }
}