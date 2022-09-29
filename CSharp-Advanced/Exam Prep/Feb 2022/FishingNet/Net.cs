namespace FishingNet
{
    using System.Text;

    public class Net
    {
        private readonly List<Fish> fish;

        public Net(string material, int capacity)
        {
            this.Material = material;
            this.Capacity = capacity;
            this.fish = new List<Fish>();
        }

        public List<Fish> Fish => this.fish;

        public string Material { get; }

        public int Capacity { get; }

        public int Count => this.fish.Count;

        public string AddFish(Fish fish)
        {
            if (this.Capacity == this.Count)
            {
                return "Fishing net is full.";
            }

            if (string.IsNullOrWhiteSpace(fish.FishType) || fish.Weight <= 0 || fish.Length <= 0)
            {
                return "Invalid fish";
            }

            this.fish.Add(fish);

            return $"Successfully added {fish.FishType} to the fishing net.";
        }

        public bool ReleaseFish(double weight)
        {
            foreach (var fish in this.fish)
            {
                if (fish.Weight == weight)
                {
                    this.fish.Remove(fish);

                    return true;
                }
            }

            return false;
        }

        public Fish GetFish(string fishType)
        {
            foreach (var fish in this.fish)
            {
                if (fish.FishType == fishType)
                {
                    return fish;
                }
            }

            return null;
        }

        public Fish GetBiggestFish()
        {
            var fish = this.fish
                .OrderByDescending(x => x.Length)
                .FirstOrDefault();

            return fish;
        }
        
        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Into the {this.Material}:");

            foreach (var fish in this.fish.OrderByDescending(x => x.Length))
            {
                sb.AppendLine(fish.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
