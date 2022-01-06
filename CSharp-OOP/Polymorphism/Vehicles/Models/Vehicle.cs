using System;
using Vehicles.Common;
using Vehicles.Contracts;

namespace Vehicles.Models
{
    public abstract class Vehicle : IRefuable, IDrivable
    {
        private double tankCapacity;

        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
            this.TankCapacity = tankCapacity;
        }

        public double FuelQuantity { get; set; }

        public virtual double FuelConsumption { get; private set; }

        public double TankCapacity
        {
            get
            {
                return this.tankCapacity;
            }
            private set
            {
                if (this.FuelQuantity > value)
                {
                    this.FuelQuantity = 0;
                }

                this.tankCapacity = value;
            }
        }

        public virtual string Drive(double distance)
        {
            double fuelNeeded = distance * this.FuelConsumption;

            if (this.FuelQuantity < fuelNeeded)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.NotEnoughFuel, this.GetType().Name));
            }

            this.FuelQuantity -= fuelNeeded;

            return string.Format(ExceptionMessages.SuccDriveMsg, this.GetType().Name, distance);
        }

        public string DriveEmpty(double distance)
        {
            return this.Drive(distance);
        }

        public virtual void Refuel(double liters)
        {

            if (liters <= 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFuel);
            }

            double result = this.FuelQuantity + liters;

            if (result > this.TankCapacity)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RefuelAboveCapacity, liters));
            }

            this.FuelQuantity += liters;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.FuelQuantity:F2}";
        }
    }
}
