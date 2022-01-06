using System.Collections.Generic;
using System.Linq;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : IRepository<IDriver>
    {
        private readonly List<IDriver> drivers;
        public DriverRepository()
        {
            this.drivers = new List<IDriver>();
        }

        public void Add(IDriver driver) => this.drivers.Add(driver);

        public IReadOnlyCollection<IDriver> GetAll() => this.drivers.ToList();

        public IDriver GetByName(string name) => this.drivers.FirstOrDefault(x => x.Name == name);

        public bool Remove(IDriver model) => this.drivers.Remove(model);
    }
}
