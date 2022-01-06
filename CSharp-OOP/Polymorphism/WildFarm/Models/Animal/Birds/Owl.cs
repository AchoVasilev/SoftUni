using System;
using WildFarm.Common;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animal.Birds
{
    public class Owl : Bird
    {
        private const double WeightGainCoef = 0.25;

        public Owl(string name, double weight, int foodEaten, double wingSize) 
            : base(name, weight, foodEaten, wingSize)
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
            return "Hoot Hoot";
        }
    }
}
