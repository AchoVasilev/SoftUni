namespace Restaurant
{
    public class Beverage : Product
    {
        private double mililiters;
        public Beverage(string name, decimal price, double mililiters) 
            : base(name, price)
        {
            this.Mililiters = mililiters;
        }

        public double Mililiters
        {
            get => this.mililiters;
            set => this.mililiters = value;
        }
    }
}
