using WildFarm.Contracts;

namespace WildFarm.Models.Animal
{
    public abstract class Mammal : Animal, IMammal
    {
        public Mammal(string name, double weight, int foodEaten, string livingRegion) 
            : base(name, weight, foodEaten)
        {
            this.LivingRegion = livingRegion;
        }

        public string LivingRegion { get; }
    }
}
