using System.Text;

using MilitaryElite.Contracts;

namespace MilitaryElite.Models
{
    public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        public SpecialisedSoldier(string id, string firstName, string lastName, decimal salary, Corps corps) : base(id, firstName, lastName, salary)
        {
            this.Corps = corps;
        }

        public Corps Corps { get; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString())
                .AppendLine($"Corps: {this.Corps}");

            return sb.ToString().Trim();
        }
    }
}
