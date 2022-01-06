namespace SantaWorkshop.Models.Dwarfs
{
    public class HappyDwarf : Dwarf
    {
        private const int initialEnergy = 100;
        public HappyDwarf(string name) 
            : base(name, initialEnergy)
        {
        }

        public override void Work()
        {
            this.Energy -= 10;
        }
    }
}
