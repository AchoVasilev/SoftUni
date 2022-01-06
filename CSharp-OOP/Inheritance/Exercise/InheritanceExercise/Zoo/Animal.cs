namespace Zoo
{
    public class Animal
    {
        private string name;
        public Animal(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get => this.name;
            private set => this.name = value;
        }
    }
}
