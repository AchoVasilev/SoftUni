using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Utilities.Messages;
using System;

namespace SantaWorkshop.Models.Presents
{
    public class Present : IPresent
    {
        private const int MinEnergy = 0;
        private const int EnergyDecreasingPoints = 10;

        private string name;
        private int energyRequired;
        public Present(string name, int energyRequired)
        {
            this.Name = name;
            this.EnergyRequired = energyRequired;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    var msg = ExceptionMessages.InvalidPresentName;
                    throw new ArgumentException(msg);
                }

                this.name = value;
            }
        }

        public int EnergyRequired
        {
            get => this.energyRequired;
            private set
            {
                if (value < MinEnergy)
                {
                    value = MinEnergy;
                }

                this.energyRequired = value;
            }
        }
        public void GetCrafted()
        {
            this.EnergyRequired -= EnergyDecreasingPoints;
        }

        public bool IsDone() => this.EnergyRequired == MinEnergy;
    }
}
