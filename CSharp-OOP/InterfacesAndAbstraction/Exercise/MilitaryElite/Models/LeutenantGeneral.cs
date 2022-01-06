using System.Text;
using System.Collections.Generic;

using MilitaryElite.Contracts;

namespace MilitaryElite.Models
{
    public class LeutenantGeneral : Soldier, ILeutenantGeneral, IPrivate
    {
        public LeutenantGeneral(string id, string firstName, string lastName, decimal salary) : base(id, firstName, lastName)
        {
            this.Salary = salary;
            this.Privates = new HashSet<IPrivate>();
        }

        public ICollection<IPrivate> Privates { get; }

        public decimal Salary { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString())
                .AppendLine($" Salary: {this.Salary:F2}")
                .AppendLine("Privates:");

            foreach(Private p in this.Privates)
            {
                sb.AppendLine("  " + p.ToString());
            }
            return sb.ToString().Trim();
        }
    }
}