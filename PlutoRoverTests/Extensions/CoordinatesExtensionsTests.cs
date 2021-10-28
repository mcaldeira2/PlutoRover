using PlutoRover.CrossCutting;
using PlutoRover.Extensions;
using Xunit;

namespace PlutoRoverTests.Extensions
{
    public class CoordinatesExtensionsTests
    {
        [Fact]
        public void ToCoordinate_WhenArrayLengthIsLessThan2_ShouldReturnError()
        {
            //Arrange
            var array = new[] { 1 };

            //Act
            var result = CoordinatesExtensions.ToCoordinate(array);

            //Assert
            Assert.True(result.HasError);
            Assert.Equal(ErrorCodes.NotEnoughElementsInArrayMsg, result.Error);
        }

        [Fact]
        public void ToCoordinate_WhenArrayLengthEqualsToTwo_ShouldReturnCoordinate()
        {
            //Arrange
            int x = 30, y = 37;
            var array = new[] { x, y };

            //Act
            var result = CoordinatesExtensions.ToCoordinate(array);

            //Assert
            Assert.Equal(x, result.Data.X);
            Assert.Equal(y, result.Data.Y);
        }

        [Fact]
        public void ToCoordinate_WhenArrayLengthIsMoreThanTwo_ShouldReturnCoordinate()
        {
            //Arrange
            int x = 30, y = 37;
            var array = new[] { x, y, 53, 30, 3, 323 };

            //Act
            var result = CoordinatesExtensions.ToCoordinate(array);

            //Assert
            Assert.Equal(x, result.Data.X);
            Assert.Equal(y, result.Data.Y);
        }

        [Fact]
        public void ToCoordinate_WhenStringIsValid_ShouldReturnCoordinate()
        {
            //Arrange
            int x = 3, y = 4;
            var input = $"{x}{CoordinatesExtensions.Separator}{y}";

            //Act
            var result = CoordinatesExtensions.ToCoordinate(input);

            //Assert
            Assert.Equal(x, result.Data.X);
            Assert.Equal(y, result.Data.Y);
        }

         [Fact]
        public void ToCoordinate_WhenStringIsNotValid_ShouldReturnError()
        {
            //Arrange
            var input = "invalidInput";

            //Act
            var result = CoordinatesExtensions.ToCoordinate(input);

            //Assert
            Assert.True(result.HasError);
            Assert.Equal(ErrorCodes.CoordinateFormatMsg, result.Error);
        }
    }
}