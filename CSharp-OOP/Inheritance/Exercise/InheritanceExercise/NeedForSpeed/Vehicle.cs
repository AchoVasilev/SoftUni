namespace NeedForSpeed
{
    public class Vehicle
    {
        private int horsePower;
        private double fuel;
        private const double defaultFuelConsumption = 1.25;

        public Vehicle(int horsePower, double fuel)
        {
            this.HorsePower = horsePower;
            this.Fuel = fuel;
        }

        public int HorsePower
        {
            get => this.horsePower;
            set => this.horsePower = value;
        }

        public double Fuel
        {
            get => this.fuel;
            set => this.fuel = value;
        }

        public virtual double FuelConsumption
        {
            get => defaultFuelConsumption;
        }

        public virtual void Drive(double kilometers)
        {
            this.Fuel -= kilometers * this.FuelConsumption;
        }
    }
}
