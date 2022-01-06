using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private readonly List<IDecoration> decorations;
        private readonly List<IFish> fishes;

        protected Aquarium(string name, int capacity)
        {
            this.decorations = new List<IDecoration>();
            this.fishes = new List<IFish>();
            this.Name = name;
            this.Capacity = capacity;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }

                this.name = value;
            }
        }
        public int Capacity { get; }
        public int Comfort => CalculateTotalComfort();


        public ICollection<IDecoration> Decorations => this.decorations.AsReadOnly();
        public ICollection<IFish> Fish => this.fishes.AsReadOnly();

        public void AddDecoration(IDecoration decoration)
        {
            this.decorations.Add(decoration);
        }
        public void AddFish(IFish fish)
        {
            if (this.fishes.Count >= this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.fishes.Add(fish);
        }
        public void Feed()
        {
            foreach (var fish in this.fishes)
            {
                fish.Eat();
            }
        }
        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} ({this.GetType().Name}):");

            string fishInfo = this.fishes.Any() ? string.Join(", ", fishes.Select(f => f.Name)) : "none";
            sb.AppendLine($"Fish: {fishInfo}")
                .AppendLine($"Decorations: {this.Decorations.Count}")
                .AppendLine($"Comfort: {this.Comfort}");

            return sb.ToString().TrimEnd();
        }
        public bool RemoveFish(IFish fish) => this.fishes.Remove(fish);

        private int CalculateTotalComfort()
        {
            int sum = this.decorations.Sum(d => d.Comfort);

            return sum;
        }
    }
}
