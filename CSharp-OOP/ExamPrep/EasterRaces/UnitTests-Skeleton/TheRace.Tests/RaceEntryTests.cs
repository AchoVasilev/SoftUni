using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddDriverMethodShouldThrowIOExceptionWhenNullIsPassed()
        {
            RaceEntry raceEntry = new RaceEntry();

            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(null));
        }

        [Test]
        public void AddDriverMethodShouldThrowIOExceptionWhenDriverAlreadyExists()
        {
            RaceEntry raceEntry = new RaceEntry();
            UnitDriver unitDriver = new UnitDriver("Gosho", new UnitCar("VW", 100, 1000));

            Assert.Throws<InvalidOperationException>(() =>
            {
                raceEntry.AddDriver(unitDriver);
                raceEntry.AddDriver(unitDriver);
            });
        }

        [Test]
        public void AddDriverMethodShouldWorkCorrectly()
        {
            RaceEntry raceEntry = new RaceEntry();
            UnitCar unitCar = new UnitCar("VW", 100, 100);
            UnitDriver unitDriver = new UnitDriver("Gosho", unitCar);

            string added = raceEntry.AddDriver(unitDriver);

            Assert.AreEqual("Driver Gosho added in race.", added);
            Assert.AreEqual(1, raceEntry.Counter);
        }

        [Test]
        public void CalculateAverageHorsepowerShouldThrowExceptionWhenParticipantsAreNotEnough()
        {
            RaceEntry raceEntry = new RaceEntry();
            UnitCar unitCar = new UnitCar("VW", 100, 100);
            UnitDriver unitDriver = new UnitDriver("Gosho", unitCar);

            string added = raceEntry.AddDriver(unitDriver);

            Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());
        }

        [Test]
        public void CalculateAverageHorsepowerShouldWorkCorrectly()
        {
            RaceEntry raceEntry = new RaceEntry();

            UnitCar unitCar = new UnitCar("VW", 100, 100);
            UnitDriver unitDriver = new UnitDriver("Gosho", unitCar);

            UnitCar unitCar2 = new UnitCar("BMW", 100, 100);
            UnitDriver unitDriver2 = new UnitDriver("Ivan", unitCar2);

            UnitCar unitCar3 = new UnitCar("Audi", 100, 100);
            UnitDriver unitDriver3 = new UnitDriver("Pesho", unitCar3);

            raceEntry.AddDriver(unitDriver);
            raceEntry.AddDriver(unitDriver2);
            raceEntry.AddDriver(unitDriver3);

            Assert.AreEqual(100, raceEntry.CalculateAverageHorsePower());
        }
    }
}