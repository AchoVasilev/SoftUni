using System;
using Vehicles.Core;
using Vehicles.Factories;
using Vehicles.IO;

namespace Vehicles
{
    class Program
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            IVehicleFactory vehicleFactory = new VehicleFactory();

            IEngine engine = new Engine(reader, writer, vehicleFactory);
            engine.Run();
        }
    }
}
