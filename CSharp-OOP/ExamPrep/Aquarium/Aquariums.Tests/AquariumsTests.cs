namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    public class AquariumsTests
    {
        private const string DefaultFishName = "Nemo";
        private const string DefaultAquariumName = "Underworld";
        private const int DefaultAquariumCapacity = 5;

        private Fish defaultFish;
        private Aquarium defaultAquarium;

        [SetUp]
        public void SetUp()
        {
            this.defaultFish = new Fish(DefaultFishName);
            this.defaultAquarium = new Aquarium(DefaultAquariumName, DefaultAquariumCapacity);
        }

        [Test]
        public void FishConstructorShouldSetCorrectValues()
        {
            Assert.AreEqual(DefaultFishName, this.defaultFish.Name);
            Assert.That(this.defaultFish.Available, Is.True);
        }

        [Test]
        public void AquariumConstructorShouldSetCorrectValues()
        {
            Assert.AreEqual(DefaultAquariumName, this.defaultAquarium.Name);
            Assert.AreEqual(DefaultAquariumCapacity, this.defaultAquarium.Capacity);
        }

        [Test]
        public void AquariumNameShouldThrowExceptionWhenNull()
        {
            Assert.Throws<ArgumentNullException>(() => this.defaultAquarium = new Aquarium(null, DefaultAquariumCapacity));
        }

        [Test]
        public void AquariumNameShouldThrowExceptionWhenEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => this.defaultAquarium = new Aquarium(string.Empty, DefaultAquariumCapacity));
        }

        [Test]
        public void AquariumCapacityShouldThrowExceptionIfValueIsNegative()
        {
            Assert.Throws<ArgumentException>(() => this.defaultAquarium = new Aquarium(DefaultAquariumName, -10));
        }

        [Test]
        public void AquariumAddMethodShouldThrowExceptionWhenAquariumIsFull()
        {
            this.AddManyFishes();
            this.defaultAquarium.Add(new Fish("Gosho"));

            Assert.Throws<InvalidOperationException>(() => this.defaultAquarium.Add(new Fish("Pesho")));
        }

        [Test]
        public void AquariumAddMethodShouldAddCorrectly()
        {
            this.defaultAquarium.Add(defaultFish);

            Assert.AreEqual(1, this.defaultAquarium.Count);
        }

        [Test]
        public void AquariumRemoveMethodShouldThrowExceptionIfFishIsNull()
        {
            Assert.Throws<InvalidOperationException>(() => this.defaultAquarium.RemoveFish("Pesho"));
        }

        [Test]
        public void AquariumRemoveMethodShouldRemoveValidFish()
        {
            this.AddManyFishes();
            this.defaultAquarium.RemoveFish(DefaultFishName);

            Assert.AreEqual(3, defaultAquarium.Count);
        }

        [Test]
        public void AquariumSellFishShouldThrowExceptionIfNameIsNull()
        {
            Assert.Throws<InvalidOperationException>(() => this.defaultAquarium.SellFish("Gosho"));
        }

        [Test]
        public void AquariumSellFishMethodShouldSetAvailableStatusToFalse()
        {
            this.AddManyFishes();

            Fish soldFish = this.defaultAquarium.SellFish(DefaultFishName);
            Assert.That(this.defaultFish.Available, Is.False);
            Assert.AreEqual(this.defaultFish, soldFish);
        }

        [Test]
        public void AquariumReportMethodShouldReturnCorrectText()
        {
            this.AddManyFishes();
            string fishes = "Nemo, Dorry, Emerald, Diamond";
            string expectedResult = $"Fish available at {this.defaultAquarium.Name}: {fishes}";

            Assert.AreEqual(expectedResult, this.defaultAquarium.Report());
        }

        [Test]
        public void ReportShouldReturnEmptyStringWithNoFishesh()
        {
            var fishInfo = string.Empty;
            var expected = $"Fish available at {this.defaultAquarium.Name}: {fishInfo}";

            Assert.AreEqual(expected, this.defaultAquarium.Report());
        }

        private void AddManyFishes()
        {
            this.defaultAquarium.Add(defaultFish);
            this.defaultAquarium.Add(new Fish("Dorry"));
            this.defaultAquarium.Add(new Fish("Emerald"));
            this.defaultAquarium.Add(new Fish("Diamond"));
        }
    }
}
