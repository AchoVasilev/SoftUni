using MilitaryElite.Models;
using System.Collections.Generic;

namespace MilitaryElite.Contracts
{
    public interface ICommando
    {
        public ICollection<Mission> Mission { get; }
    }
}
