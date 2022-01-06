using System.Text;
using System.Collections.Generic;

using MilitaryElite.Contracts;

namespace MilitaryElite.Models
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        public Commando(string id, string firstName, string lastName, decimal salary, Corps corps) 
            : base(id, firstName, lastName, salary, corps)
        {
            this.Mission = new HashSet<Mission>();
        }

        public ICollection<Mission> Mission { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString())
                .AppendLine("Missions:");

            foreach (var mission in this.Mission)
            {
                sb.AppendLine("  " + mission.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
