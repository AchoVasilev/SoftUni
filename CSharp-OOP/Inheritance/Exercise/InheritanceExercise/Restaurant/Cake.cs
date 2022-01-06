namespace Restaurant
{
    public class Cake : Dessert
    {
        private const double defaultGrams = 500;
        private const double defaultCalories = 1000;
        private const decimal defaultPrice = 5m;
        public Cake(string name) : base(name, defaultPrice, defaultGrams, defaultCalories)
        {
        }
    }
}
