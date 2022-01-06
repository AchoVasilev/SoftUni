using System.Text;
using WildFarm.Contracts;

namespace WildFarm.Models.Animal
{
    public abstract class Feline : Mammal, IFeline
    {
        public Feline(string name, double weight, int foodEaten, string livingRegion, string breed) 
            : base(name, weight, foodEaten, livingRegion)
        {
            this.Breed = breed;
        }

        public string Breed { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.ToString())
                .AppendLine($"{this.Breed}, {this.Weight}, {this.LivingRegion}, {this.FoodEaten}]");

            return sb.ToString().Trim();
        }
    }
}
