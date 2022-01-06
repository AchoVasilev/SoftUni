using Vehicles.Models;

namespace Vehicles.Factories
{
    public interface IVehicleFactory
    {
        IVehicle CreateVehicle(string type, double fuelQuantity, double fuelConsumption, bool hasAirConditioner = true);
    }
}
