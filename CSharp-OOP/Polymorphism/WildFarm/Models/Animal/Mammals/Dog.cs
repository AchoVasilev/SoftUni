using System;
using System.Text;
using WildFarm.Common;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animal.Mammals
{
    public class Dog : Mammal
    {
        private const double WeightGainCoef = 0.40;
        public Dog(string name, double weight, int foodEaten, string livingRegion) 
            : base(name, weight, foodEaten, livingRegion)
        {
        }

        public override double EatFood(Food food)
        {
            if (food.GetType().Name != "Meat")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidAnimalFoodType, this.GetType().Name, food.GetType().Name));
            }

            return this.Weight += (food.Quantity * WeightGainCoef);
        }

        public override string ProduceSound()
        {
            return "Woof!";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.ToString())
                .AppendLine($"{this.Weight}, {this.LivingRegion}, {this.FoodEaten}]");

            return sb.ToString().Trim();
        }
    }
}
