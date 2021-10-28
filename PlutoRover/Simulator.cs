using PlutoRover.Domain.Maps;
using PlutoRover.Domain.Models;

namespace PlutoRover
{
    // Just a class to aggregate both grid and vehicle
    public class Simulator
    {
        public IGrid Grid { get; private set; }

        public IVehicle Vehicle {get; private set; }

        public Simulator()
        {
            Grid = new Grid();
            Vehicle = new Vehicle(coordinate => Grid.CanMove(coordinate));
        }

        public void SetGrid(IGrid grid) => Grid = grid;

        public void SetVehicle(IVehicle vehicle) => Vehicle = vehicle;
    }
}