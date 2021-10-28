using System;
using PlutoRover.Domain.Enums;
using PlutoRover.Extensions;
using PlutoRover.Domain.Maps;
using PlutoRover.Domain.Models;
using PlutoRover.CrossCutting.Constants;
using PlutoRover.Domain.ValueObjects;

namespace PlutoRover
{
    // Just a class to present our User Interface
    public class UI
    {
        public Simulator Simulator { get; private set; } = new Simulator();

        public void RenderHelp()
        {
            Console.Clear();
            Console.WriteLine("Help Menu:");
            Console.WriteLine("Use the following commands to move rover around the planet: ");
            Console.WriteLine("f -> moves forward");
            Console.WriteLine("b -> moves backwards");
            Console.WriteLine("l -> rotates 90 degrees to the left");
            Console.WriteLine("r -> rotates 90 degrees to the right");
            Console.WriteLine("h -> instructions history");
            Console.WriteLine("q -> ends simulation");
            Console.WriteLine("Type any key to go back");
        }

        public void RenderMenu()
        {
            var shouldRenderMenu = true;
            
            while (shouldRenderMenu)
            {
                Console.Clear();
                Console.WriteLine("Hello Nasa, Let's build our simulation together.");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1) Start");
                Console.WriteLine("2) Settings");
                Console.WriteLine("3) Exit");
                Console.Write("\r\nSelect an option: ");
                
                var input = Console.ReadLine();

                var result = input switch
                {
                    StringConstants.One => () => { shouldRenderMenu = false;},
                    StringConstants.Two => () => RenderSettings(),
                    StringConstants.Three => () => Environment.Exit(1),
                    _ => ActionConstants.Empty
                };

                result();
            }
        }

        public IGrid SetGridSize(IGrid grid)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Specify the grid (max width, max height)");

                var input = Console.ReadLine();
                var coordinates = input.ToCoordinate();

                if (!coordinates.HasError)
                {
                    grid.ChangeSize(coordinates.Data.X, coordinates.Data.Y);
                    break;
                }

                Console.Clear();
                Console.WriteLine(coordinates.Error.Msg);
            }

            Console.WriteLine("Grid created successfully.");

            return grid;
        }

        public IGrid SpawnObjects(IGrid grid)
        {
            Console.Clear();

            while (true)
            {

                Console.WriteLine("Specify the coordinates for your obstacle (X,Y). When you're done type 'done' ofcourse.");

                var input = Console.ReadLine();

                if (input == "done")
                {
                    break;
                }

                var coordinates = input.ToCoordinate();

                if (coordinates.HasError)
                {
                    Console.Clear();
                    Console.WriteLine(coordinates.Error.Msg);
                }
                else
                {
                    var result = grid.Spawn(new Rock(new Coordinate(coordinates.Data.X, coordinates.Data.Y)));

                    if(result.HasError)
                    {
                        Console.WriteLine(result.Error.Msg);
                    }
                }
            }

            return grid;
        }

        public (IGrid grid, IVehicle vehicle) SpawnRover(IGrid grid, IVehicle vehicle)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Specify the coordinates to land it. (x,Y)");

                var coordinates = Console.ReadLine().ToCoordinate();

                if (coordinates.HasError)
                {
                    Console.Clear();
                    Console.WriteLine(coordinates.Error.Msg);
                }
                else
                {
                    vehicle = new Vehicle(Direction.North, new Coordinate(coordinates.Data.X, coordinates.Data.Y), coord => grid.CanMove(coord));

                    var spawnResult = grid.Spawn(vehicle);

                    if (!spawnResult.HasError)
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(spawnResult.Error.Msg);
                    }
                }
            }

            return (grid, vehicle);
        }

        public void RenderSettings()
        {
            var grid = Simulator.Grid;
            var vehicle = Simulator.Vehicle;

            var settingInput = string.Empty;

            while (settingInput != StringConstants.Four)
            {

                Console.Clear();
                Console.WriteLine("Settings:");
                Console.WriteLine("1) Set Grid size");
                Console.WriteLine("2) Spawn obstacles");
                Console.WriteLine("3) Set rover spawn coordinates");
                Console.WriteLine("4) Exit");
                Console.Write("\r\nSelect an option: ");
                settingInput = Console.ReadLine();

                if (settingInput == StringConstants.One)
                {
                    grid = SetGridSize(grid);
                }

                if (settingInput == StringConstants.Two)
                {
                    grid = SpawnObjects(grid);
                }

                if (settingInput == StringConstants.Three)
                {
                    (grid, vehicle) = SpawnRover(grid, vehicle);
                }

                if (settingInput == StringConstants.Four)
                {
                    break;
                }
            }

            Simulator.SetGrid(grid);
            Simulator.SetVehicle(vehicle);
        }
    }
}