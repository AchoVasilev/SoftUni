using System;
using WildFarm.Common;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animal.Mammals.Felines
{
    public class Cat : Feline
    {
        private const double WeightGainCoef = 0.30;

        public Cat(string name, double weight, int foodEaten, string livingRegion, string breed) 
            : base(name, weight, foodEaten, livingRegion, breed)
        {
        }

        public override double EatFood(Food food)
        {
            if (food.GetType().Name != "Meat" && food.GetType().Name != "Vegetable")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidAnimalFoodType, this.GetType().Name, food.GetType().Name));
            }

            return this.Weight += (food.Quantity * WeightGainCoef);
        }

        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
