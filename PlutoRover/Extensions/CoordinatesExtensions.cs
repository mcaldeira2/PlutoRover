using System.Linq;
using System.Text.RegularExpressions;
using PlutoRover.CrossCutting;
using PlutoRover.Domain.ValueObjects;

namespace PlutoRover.Extensions
{
    public static class CoordinatesExtensions
    {
        public const char Separator = ',';

        public const string CoordinateRegex = @"\d*\,\d*";

        // Creates Coordinate object from an integer array
        public static Result<Coordinate> ToCoordinate(this int[] array)
        {
            if (array.Length < 2)
            {
                return new Result<Coordinate>(ErrorCodes.NotEnoughElementsInArrayMsg);
            }

            return new Result<Coordinate>(new Coordinate(array[0], array[1]));
        }

        // Creates a Coordinate from a string (x,y)
        public static Result<Coordinate> ToCoordinate(this string input)
        {
            var result = Regex.Match(input, CoordinateRegex);

            if (!result.Success)
            {
                return new Result<Coordinate>(ErrorCodes.CoordinateFormatMsg);
            }

            var coordinateResult = result.Value.Split(Separator)
                 .Select(input => { int.TryParse(input, out var result); return result; })
                 .ToArray()
                 .ToCoordinate();

            return coordinateResult.HasError ? new Result<Coordinate>(ErrorCodes.CoordinateFormatMsg) : coordinateResult;
        }
    }
}