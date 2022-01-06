using WildFarm.Models.Animal;
using WildFarm.Models.Animal.Mammals.Felines;

namespace WildFarm.Factories
{
    public class FelineFactory
    {
        public FelineFactory()
        {

        }

        public Feline CreateFeline(string type, string name, double weight, string livingRegion, string breed)
        {
            Feline feline;

            if (type == "Tiger")
            {
                feline = new Tiger(name, weight,)
            }
            else if (type == "Cat")
            {

            }
        }
    }
}
