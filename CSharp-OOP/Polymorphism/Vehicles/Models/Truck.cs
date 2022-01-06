namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double FuelConsIncr = 1.6;
        private const double LostRefuelCoef = 0.95;

        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity) 
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {

        }

        public override double FuelConsumption => base.FuelConsumption + FuelConsIncr;

        public override void Refuel(double liters)
        {
            base.Refuel(liters * LostRefuelCoef);
        }
    }
}
