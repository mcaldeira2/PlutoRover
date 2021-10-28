using PlutoRover.Domain.Models;
using PlutoRover.CrossCutting;
using PlutoRover.Domain.ValueObjects;

namespace PlutoRover.Domain.Maps
{
    public interface IGrid
    {
        int MaxX { get; }
        
        int MaxY { get; }

        int MinX { get; }

        int MinY { get; }

        // Mutates grid size
        // TODO: Prevent negative integers with PositiveNumber valuetype
        void ChangeSize(int width, int height);

        // Checks whether specified coordinate has an object
        bool CanMove(Coordinate solidObject);

        // Spawns some object in the grid
        Result<Coordinate> Spawn(ISolidObject solidObject);
    }
}