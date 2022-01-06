using FootballTeamGenerator.Common;
using System;

namespace FootballTeamGenerator.Models
{
    public class Player
    {
        private string name;

        public Player(string name, Stats stats)
        {
            this.Name = name;
            this.Stats = stats;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(GlobalConstants.IvalidNameExcMsg);
                }

                this.name = value;
            }
        }

        public Stats Stats { get; set; }

        public double OverallSkill => this.Stats.AverageStat;
    }
}
