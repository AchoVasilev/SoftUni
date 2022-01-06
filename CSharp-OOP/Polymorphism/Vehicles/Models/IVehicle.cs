using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    public interface IVehicle
    {
        double FuelQuantity { get; }
        double FuelConsumption { get; }
        bool HasAirConditioner { get; }

        double AirConditionerFuelConsumption { get; }
        bool Drive(double distance);
        void Refuel(double liters);
    }
}
