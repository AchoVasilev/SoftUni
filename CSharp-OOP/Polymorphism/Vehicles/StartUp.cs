using Vehicles.Contracts;
using Vehicles.Core;

namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();

            engine.Run();
        }
    }
}
