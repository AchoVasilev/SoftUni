using SantaWorkshop.Models.Instruments.Contracts;

namespace SantaWorkshop.Models.Instruments
{
    public class Instrument : IInstrument
    {
        private const int MinPower = 0;
        private const int PowerDecreasingPoints = 10;

        private int power;

        public Instrument(int power)
        {
            this.Power = power;
        }

        public int Power
        {
            get => this.power;
            private set
            {
                if (value < MinPower)
                {
                    value = MinPower;
                }

                this.power = value;
            }
        }
        public void Use()
        {
            this.Power -= PowerDecreasingPoints;
        }

        public bool IsBroken() => this.Power == MinPower;
    }
}
