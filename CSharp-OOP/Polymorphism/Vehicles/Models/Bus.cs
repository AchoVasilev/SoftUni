using System;
using Vehicles.Common;

namespace Vehicles.Models
{
    public class Bus : Vehicle
    {
        private const double FuelConsumptionIncr = 1.4;

        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) 
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override string Drive(double distance)
        {
            double fuelNeeded = distance * (this.FuelConsumption + FuelConsumptionIncr);

            if (this.FuelQuantity < fuelNeeded)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.NotEnoughFuel, this.GetType().Name));
            }

            this.FuelQuantity -= fuelNeeded;

            return string.Format(ExceptionMessages.SuccDriveMsg, this.GetType().Name, distance);
        }
    }
}
