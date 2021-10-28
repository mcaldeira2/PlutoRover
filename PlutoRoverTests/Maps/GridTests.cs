using PlutoRover.CrossCutting;
using PlutoRover.Domain.Maps;
using PlutoRover.Domain.Models;
using PlutoRover.Domain.ValueObjects;
using Xunit;

namespace PlutoRoverTests.Maps
{
    public class GridTests
    {
        [Fact]
        public void Spawn_WhenCoordinatesIsFree_ShouldSpawnObject()
        {
            //Arrange
            var grid = new Grid();

            //Act
            var result = grid.Spawn(new Rock(new Coordinate(1,1)));

            //Assert
            Assert.False(result.HasError);
        }

        [Theory]
        [InlineData(3, 3, 4, 3)]
        [InlineData(3, 3, 3, 4)]
        [InlineData(2, 2, 3, 3)]
        public void Spawn_WhenCoordinateIsOutsideGridBounds_ShouldReturnError(int gridWidth, int gridHeight, int x, int y)
        {
            //Arrange
            var grid = new Grid(gridWidth, gridHeight);

            //Act
            var result = grid.Spawn(new Rock(new Coordinate(x, y)));

            //Assert
            Assert.True(result.HasError);
            Assert.Equal(ErrorCodes.CoordinateOutsideGridBoundsMsg, result.Error);
        }

        [Fact]
        public void Spawn_WhenCoordinateAlreadyConstainsObject_ShouldReturnError()
        {
            //Arrange
            var grid = new Grid();
            grid.Spawn(new Rock(new Coordinate(1,1)));

            //Act
            var result = grid.Spawn(new Rock(new Coordinate(1,1)));

            //Assert
            Assert.True(result.HasError);
            Assert.Equal(ErrorCodes.SolidObjectAlreadyOnCoordsMsg, result.Error);
        }

        [Fact]
        public void CanMove_WhenCoordinatesIsFree_ShouldReturnTrue()
        {
            //Arrange
            var grid = new Grid();

            //Act
            var result = grid.CanMove(new Coordinate(1,1));

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(3, 3, 4, 3)]
        [InlineData(3, 3, 3, 4)]
        [InlineData(2, 2, 3, 3)]
        public void CanMove_WhenCoordinateIsOutsideGridBounds_ShouldReturnFalse(int gridWidth, int gridHeight, int x, int y)
        {
            //Arrange
            var grid = new Grid(gridWidth, gridHeight);

            //Act
            var result = grid.CanMove(new Coordinate(x, y));

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void CanMove_WhenCoordinateAlreadyConstainsObject_ShouldReturnFalse()
        {
            //Arrange
            var grid = new Grid();
            grid.Spawn(new Rock(new Coordinate(1,1)));

            //Act
            var result = grid.CanMove(new Coordinate(1,1));

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ChangeSize_ShouldChangeMaxXandMaxY()
        {
            //Arrange
            var newWidth = 2;
            var newHeight = 2;
            var grid = new Grid(3,3);

            //Act
            grid.ChangeSize(newWidth, newHeight);

            //Assert
            Assert.Equal(newWidth, grid.MaxX);
            Assert.Equal(newHeight, grid.MaxY);
        }

        
    }
}