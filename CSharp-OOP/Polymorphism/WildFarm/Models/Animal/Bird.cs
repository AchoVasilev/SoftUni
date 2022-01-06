using System.Text;
using WildFarm.Contracts;

namespace WildFarm.Models.Animal
{
    public abstract class Bird : Animal, IBird
    {
        
        protected Bird(string name, double weight, int foodEaten, double wingSize) 
            : base(name, weight, foodEaten)
        {
            this.WingSize = wingSize;
        }

        public double WingSize { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString())
                .AppendLine($"{this.WingSize}, {this.Weight}, {this.FoodEaten}]");

            return sb.ToString().Trim();
        }
    }
}
