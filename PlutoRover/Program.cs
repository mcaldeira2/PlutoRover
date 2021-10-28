using System;
using System.Linq;

namespace PlutoRover
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ui = new UI();

            Console.WriteLine("Hello Nasa, Let's build our simulation together.");

            ui.RenderMenu();

            Console.Clear();

            Console.WriteLine("Hi Nasa, I'm your Rover and I'm ready to explore Pluto!");

            // Move Rover around the planet
            Console.WriteLine("Give me the intructions to move myself around this planet.");

            bool running = true;

            while (running)
            {
                var input = Console.ReadLine();
                switch (input)
                {
                    case "help":
                        ui.RenderHelp();
                        break;
                    case "q":
                        running = false;
                        break;
                    case "h":
                        Console.WriteLine(ui.Simulator.Vehicle.ReportHistory());
                        break;
                    default:
                        var result = ui.Simulator.Vehicle.Move(input);

                        Console.Clear();

                        if (result.HasError)
                        {
                            Console.WriteLine(result.Error.Msg);
                        }

                        Console.WriteLine(ui.Simulator.Vehicle.ReportCoordinates());
                        break;
                }
            }

            Console.WriteLine("Simulation ended...");
            Console.WriteLine("Type enter to exit...");
            Console.ReadLine();
        }
    }
}
