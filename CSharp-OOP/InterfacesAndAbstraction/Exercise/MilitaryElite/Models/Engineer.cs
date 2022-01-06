using System.Text;
using System.Collections.Generic;

using MilitaryElite.Contracts;

namespace MilitaryElite.Models
{
    public class Engineer : SpecialisedSoldier, IEngineer
    {
        public Engineer(string id, string firstName, string lastName, decimal salary, Corps corps) 
            : base(id, firstName, lastName, salary, corps)
        {
            this.Repair = new HashSet<Repair>();
        }

        public ICollection<Repair> Repair { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString())
                .AppendLine("Repairs:");

            foreach (var repair in this.Repair)
            {
                sb.AppendLine("  " + repair.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
