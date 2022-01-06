using System;
using System.Text;
using System.Collections.Generic;
using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories;
using AquaShop.Repositories.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Utilities.Messages;
using System.Linq;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Models.Fish;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private IRepository<IDecoration> decorations;
        private List<IAquarium> aquariums;
        public Controller()
        {
            this.decorations = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium currentAquarium = aquariumType switch
            {
                "FreshwaterAquarium" => new FreshwaterAquarium(aquariumName),
                "SaltwaterAquarium" => new SaltwaterAquarium(aquariumName),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType)
            };

            this.aquariums.Add(currentAquarium);
            string outputMsg = string.Format(OutputMessages.SuccessfullyAdded, currentAquarium.GetType().Name);

            return outputMsg;
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration = decorationType switch
            {
                "Ornament" => new Ornament(),
                "Plant" => new Plant(),
                _=> throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType)
            };

            this.decorations.Add(decoration);
            string outputMsg = string.Format(OutputMessages.SuccessfullyAdded, decoration.GetType().Name);

            return outputMsg;
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish = null;
            string possibleAquarium = string.Empty;

            switch (fishType)
            {
                case "FreshwaterFish":
                    fish = new FreshwaterFish(fishName, fishSpecies, price);
                    possibleAquarium = "FreshwaterAquarium";
                    break;
                case "SaltwaterFish":
                    fish = new SaltwaterFish(fishName, fishSpecies, price);
                    possibleAquarium = "SaltwaterAquarium";
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            IAquarium aquarium = this.aquariums.First(a => a.Name == aquariumName);
            string outputMsg = string.Empty;

            if (possibleAquarium != aquarium.GetType().Name)
            {
                outputMsg = OutputMessages.UnsuitableWater;
            }
            else
            {
                aquarium.AddFish(fish);
                outputMsg = string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            }

            return outputMsg;
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = this.aquariums.First(a => a.Name == aquariumName);

            decimal sum = aquarium.Fish.Sum(f => f.Price) + aquarium.Decorations.Sum(d => d.Price);
            string outputMsg = string.Format(OutputMessages.AquariumValue, aquariumName, $"{sum:F2}");

            return outputMsg;
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = this.aquariums.First(a => a.Name == aquariumName);
            aquarium.Feed();

            int fishCount = aquarium.Fish.Count();
            string outputMsg = string.Format(OutputMessages.FishFed, fishCount);

            return outputMsg;
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            if (this.decorations.Models.All(d => d.GetType().Name != decorationType))
            {
                string errorMsg = string.Format(ExceptionMessages.InexistentDecoration, decorationType);

                throw new InvalidOperationException(errorMsg);
            }

            IAquarium aquarium = this.aquariums.First(a => a.Name == aquariumName);
            IDecoration decoration = this.decorations.FindByType(decorationType);

            aquarium.AddDecoration(decoration);
            this.decorations.Remove(decoration);
            string outputMsg = string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);

            return outputMsg;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var aquarium in this.aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
