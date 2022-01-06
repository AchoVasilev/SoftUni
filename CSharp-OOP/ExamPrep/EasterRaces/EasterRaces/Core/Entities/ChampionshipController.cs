using System;
using System.Text;
using System.Collections.Generic;

using EasterRaces.Core.Contracts;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Entities;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Utilities.Messages;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Races.Entities;
using System.Linq;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private readonly IRepository<IDriver> driverRepo;
        private readonly IRepository<ICar> carRepo;
        private readonly IRepository<IRace> raceRepo;
        public ChampionshipController()
        {
            this.driverRepo = new DriverRepository();
            this.carRepo = new CarRepository();
            this.raceRepo = new RaceRepository();
        }

        public string CreateDriver(string driverName)
        {
            var driverExists = this.driverRepo.GetByName(driverName);

            if (driverExists != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }

            IDriver driver = new Driver(driverName);
            driverRepo.Add(driver);

            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if (this.carRepo.GetByName(model) != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
            }

            ICar car = null;

            switch (type)
            {
                case "Muscle":
                    car = new MuscleCar(model, horsePower);
                    break;
                case "Sports":
                    car = new SportsCar(model, horsePower);
                    break;
            }

            this.carRepo.Add(car);

            return string.Format(OutputMessages.CarCreated, car.GetType().Name, model);
        }

        public string AddCarToDriver(string driverName, string carModel)
        {
            ICar car = this.carRepo.GetByName(carModel);
            IDriver driver = this.driverRepo.GetByName(driverName);

            if (car == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }

            if (driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            driver.AddCar(car);

            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = this.raceRepo.GetByName(raceName);
            IDriver driver = this.driverRepo.GetByName(driverName);

            if (race == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            race.AddDriver(driver);

            return string.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string CreateRace(string name, int laps)
        {
            IRace currentRace = this.raceRepo.GetByName(name);

            if (currentRace != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }

            IRace race = new Race(name, laps);

            this.raceRepo.Add(race);

            return string.Format(OutputMessages.RaceCreated, name);
        }


        public string StartRace(string raceName)
        {
            IRace currentRace = this.raceRepo.GetByName(raceName);

            if (currentRace == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (currentRace.Drivers.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, 3));
            }

            var drivers = currentRace.Drivers
                .OrderByDescending(x => x.Car.CalculateRacePoints(currentRace.Laps))
                .ToList();

            this.raceRepo.Remove(currentRace);

            IDriver firstDriver = drivers[0];
            IDriver secondDriver = drivers[1];
            IDriver thirdDriver = drivers[2];

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Driver {firstDriver.Name} wins {raceName} race.")
                .AppendLine($"Driver {secondDriver.Name} is second in {raceName} race.")
                .AppendLine($"Driver {thirdDriver.Name} is third in {raceName} race.");

            return sb.ToString().Trim();
        }
    }
}
