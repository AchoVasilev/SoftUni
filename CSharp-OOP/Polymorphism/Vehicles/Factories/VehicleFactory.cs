using System;

using Vehicles.Common;
using Vehicles.Models;

namespace Vehicles.Factories
{
    public class VehicleFactory
    {
        public VehicleFactory()
        {

        }
        public Vehicle CreateVehicle(string vehicleType, double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            Vehicle vehicle;

            if (vehicleType == "Car")
            {
                vehicle = new Car(fuelQuantity, fuelConsumption, tankCapacity);
            }
            else if (vehicleType == "Truck")
            {
                vehicle = new Truck(fuelQuantity, fuelConsumption, tankCapacity);
            }
            else if (true)
            {
                vehicle = new Bus(fuelQuantity, fuelConsumption, tankCapacity);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidVehicleType);
            }

            return vehicle;
        }
    }
}
