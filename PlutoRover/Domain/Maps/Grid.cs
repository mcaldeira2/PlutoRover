using System.Collections.Generic;
using System.Linq;
using PlutoRover.Domain.Models;
using PlutoRover.CrossCutting;
using PlutoRover.Domain.ValueObjects;

namespace PlutoRover.Domain.Maps
{
    public class Grid : IGrid
    {
        public int MaxX { get; private set; }

        public int MaxY { get; private set; }

        public int MinX => 0;

        public int MinY => 0;

        public List<ISolidObject> SolidObjects { get; private set; }

        public Grid() => (MaxX, MaxY, SolidObjects) = (5, 5, new List<ISolidObject>());

        public Grid(int width, int height) => (MaxX, MaxY, SolidObjects) = (width, height, new List<ISolidObject>());

        // Mutates grid size
        // TODO: Prevent negative integers with PositiveNumber valuetype
        public void ChangeSize(int width, int height) => (MaxX, MaxY) = (width, height);

        // Tries to spawn an object if the specified coordinate doesn't have one
        public Result<Coordinate> Spawn(ISolidObject solidObject)
        {
            if (!IsInBounds(solidObject.Coordinate))
            {
                return new Result<Coordinate>(ErrorCodes.CoordinateOutsideGridBoundsMsg);
            }

            if (SolidObjects.Exists(o => o.Coordinate == solidObject.Coordinate))
            {
                return new Result<Coordinate>(ErrorCodes.SolidObjectAlreadyOnCoordsMsg);
            }

            SolidObjects.Add(solidObject);

            return new Result<Coordinate>(solidObject.Coordinate);
        }

        // Checks whether specified coordinate has an object
        public bool CanMove(Coordinate coordinate)
        {
            if (!IsInBounds(coordinate) || SolidObjects.Any(obj => obj.Coordinate == coordinate))
            {
                return false;
            }

            return true;
        }

        // Checks where coordinate is inside grid bounds
        protected bool IsInBounds(Coordinate coordinate)
        => (coordinate.X >= MinX && coordinate.X <= MaxX) && (coordinate.Y >= MinY && coordinate.Y <= MaxY);
    }
}