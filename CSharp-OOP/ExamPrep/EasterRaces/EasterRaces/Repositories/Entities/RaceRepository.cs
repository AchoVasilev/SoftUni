using System.Linq;
using System.Collections.Generic;

using EasterRaces.Models.Races.Contracts;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Repositories.Entities
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> races;

        public RaceRepository()
        {
            this.races = new List<IRace>();
        }

        public void Add(IRace race) => this.races.Add(race);

        public IReadOnlyCollection<IRace> GetAll() => this.races.ToList();

        public IRace GetByName(string name) => this.races.FirstOrDefault(x => x.Name == name);

        public bool Remove(IRace race) => this.races.Remove(race);
    }
}
