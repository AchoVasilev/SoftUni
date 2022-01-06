using WildFarm.Models.Foods;

namespace WildFarm.Models.Animal.Birds
{
    public class Hen : Bird
    {
        private const double WeightGainCoef = 0.35;

        public Hen(string name, double weight, double wingSize) 
            : base(name, weight, wingSize)
        {
        }

        public override double EatFood(Food food)
        {
            return this.Weight += (food.Quantity * WeightGainCoef);
        }

        public override string ProduceSound()
        {
            return "Cluck";
        }
    }
}
