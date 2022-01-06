namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class PresentsTests
    {
        private const string DefaultPresentName = "Car";
        private const int DefaultMagicValue = 50;
        private Present defaultPresent;
        private Bag bag;
        private List<Present> presents;

        [SetUp]
        public void SetUp()
        {
            this.defaultPresent = new Present(DefaultPresentName, DefaultMagicValue);
            this.bag = new Bag();
            this.presents = new List<Present>();
        }

        [Test]
        public void PresentConstructorShouldSetCorrectValues()
        {
            Assert.AreEqual(DefaultPresentName, this.defaultPresent.Name);
            Assert.AreEqual(DefaultMagicValue, this.defaultPresent.Magic);
        }

        [Test]
        public void BagCreateMethodShouldThrowArgumentNullExceptionIfPresentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => this.bag.Create(null));
        }

        [Test]
        public void BagCreateMethodShouldThrowInvalidOperationExceptionIfPresentExists()
        {
            this.bag.Create(this.defaultPresent);
            Assert.Throws<InvalidOperationException>(() => this.bag.Create(this.defaultPresent));
        }

        [Test]
        public void BagCreateMethodShouldAddPresentsProperly()
        {
            this.presents.Add(defaultPresent);

            Assert.AreEqual(1, this.presents.Count);
        }

        [Test]
        public void BagCreateMethodShouldReturnCorrectMessageWhenPresentIsAdded()
        {
            string expectedResult = $"Successfully added present {DefaultPresentName}.";
            string actualResult = this.bag.Create(this.defaultPresent);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void BagRemoveMethodShouldRemovePresentsCorrectly()
        {
            this.bag.Create(this.defaultPresent);
            bool actualResult = this.bag.Remove(defaultPresent);
            bool expectedResult = true;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void BagGetPresentWithLeastMagicShouldWorkCorrectly()
        {
            this.bag.Create(defaultPresent);
            this.bag.Create(new Present("Hat", 20));
            this.bag.Create(new Present("Scarf", 10));

            Present present = this.bag.GetPresentWithLeastMagic();
            Assert.AreEqual("Scarf", present.Name);
            Assert.AreEqual(10, present.Magic);
        }

        [Test]
        public void BagGetPresentMethodShouldWorkCorrectly()
        {
            string name = "Hat";
            int magic = 20;

            this.bag.Create(defaultPresent);
            this.bag.Create(new Present("Hat", 20));
            this.bag.Create(new Present("Scarf", 10));

            Present present = this.bag.GetPresent(name);

            Assert.AreEqual(name, present.Name);
            Assert.AreEqual(magic, present.Magic);
        }

        [Test]
        public void BagGetPresentMethodShouldReturnNullWhenNoMatch()
        {
            var present = bag.GetPresent(DefaultPresentName);

            Assert.That(present, Is.Null);
        }

        [Test]
        public void BagGetPresentsShouldReturnCorrectCollection()
        {
            this.bag.Create(defaultPresent);
            this.bag.Create(new Present("Hat", 20));
            this.bag.Create(new Present("Scarf", 10));

            IReadOnlyCollection<Present> presentsToGet = this.bag.GetPresents();

            Assert.AreEqual(3, presentsToGet.Count);
            Assert.That(this.bag.GetPresents() != null);
        }

        [Test]
        public void BagGetPresentsMethodShouldReturnEmptyCollectionWhenNoPresentsAreAdded()
        {
            IReadOnlyCollection<Present> presentsToGet = this.bag.GetPresents();

            Assert.That(presentsToGet, Is.Empty);
        }
    }
}
