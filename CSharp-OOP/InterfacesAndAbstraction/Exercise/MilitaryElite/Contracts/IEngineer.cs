using MilitaryElite.Models;
using System.Collections.Generic;

namespace MilitaryElite.Contracts
{
    public interface IEngineer
    {
        public ICollection<Repair> Repair { get; }
    }
}
