using WildFarm.Contracts;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animal
{
    public abstract class Animal : IAnimal
    {
        protected Animal(string name, double weight, int foodEaten)
        {
            this.Name = name;
            this.Weight = weight;
            this.FoodEaten = foodEaten;
        }

        public string Name { get; }

        public double Weight { get; protected set; }

        public int FoodEaten { get; }

        public abstract double EatFood(Food food);

        public abstract string ProduceSound();

        public override string ToString()
        {
            return $"{this.GetType().Name} [{this.Name}, ";
        }
    }
}
