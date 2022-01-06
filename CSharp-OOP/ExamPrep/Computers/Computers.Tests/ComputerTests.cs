namespace Computers.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    public class ComputerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorSetCorrectNameProperty()
        {
            Computer computer = new Computer("A");

            Assert.AreEqual("A", computer.Name);
        }

        [Test]
        public void ConstructorPartsCollectionIsEmpty()
        {
            Computer computer = new Computer("a");

            Assert.IsEmpty(computer.Parts);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void NamePropertyEmptyValueShouldThrowArgumentNullException(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Computer(name));
        }

        [Test]
        public void PartsPropertyAddTwoPartsShouldAddTwoParts()
        {
            Computer computer = new Computer("a");
            computer.AddPart(new Part("d", 2));
            computer.AddPart(new Part("x", 3.1m));

            Assert.AreEqual(2, computer.Parts.Count);
        }

        [Test]
        public void TotalPricePropertyShouldReturCorrectResult()
        {
            Computer computer = new Computer("a");
            computer.AddPart(new Part("x", 2));
            computer.AddPart(new Part("y", 3));
            computer.AddPart(new Part("z", 4));

            Assert.AreEqual(9, computer.TotalPrice);
        }

        [Test]
        public void AddPartMethodNullPartShouldThrowInvalidOperationException()
        {
            Computer computer = new Computer("a");

            Assert.Throws<InvalidOperationException>(() => computer.AddPart(null));
        }

        [Test]
        public void AddPartMethodShouldAddPartCorrectly()
        {
            Computer computer = new Computer("a");
            computer.AddPart(new Part("x", 2));
            computer.AddPart(new Part("y", 3));
            computer.AddPart(new Part("z", 4));

            Assert.AreEqual(3, computer.Parts.Count);
        }

        [Test]
        public void AddPartMethodShouldAddCorrectPart()
        {
            Computer computer = new Computer("x");
            computer.AddPart(new Part("z", 3));

            Part actualPart = computer.Parts.FirstOrDefault(p => p.Name == "z");

            Assert.IsNotNull(actualPart);
        }

        [Test]
        public void RemovePartMethodShouldRemoveValidPartAndReturnTrue()
        {
            Computer computer = new Computer("q");
            Part part = new Part("e", 5);
            computer.AddPart(part);

            bool actualResult = computer.RemovePart(part);
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void RemovePartMethodWithInvalidPartShouldReturnFalse()
        {
            Computer computer = new Computer("t");
            Part part = new Part("e", 5);
            Part partTwo = new Part("q", 3);

            computer.AddPart(part);
            bool actualResult = computer.RemovePart(partTwo);
            Assert.IsFalse(actualResult);
        }

        [Test]
        public void RemovePartMethodShouldRemovePartCorrectly()
        {
            Computer computer = new Computer("a");
            Part part = new Part("x", 3);

            computer.AddPart(part);
            computer.RemovePart(part);

            Assert.AreEqual(0, computer.Parts.Count);
        }

        [Test]
        public void RemovePartMethodShouldRemoveActualPartSuccessfuly()
        {
            Computer computer = new Computer("z");
            Part part = new Part("x", 3);
            computer.AddPart(part);

            computer.RemovePart(part);
            Part actualPart = computer.Parts
                .FirstOrDefault(p => p.Name == "x");

            Assert.IsNull(actualPart);
        }

        [Test]
        public void GetPartMethodShouldReturnCorrectValidPart()
        {
            Computer computer = new Computer("x");
            Part part = new Part("z", 2);
            computer.AddPart(part);

            Part actualPart = computer.GetPart("z");

            Assert.AreEqual("z", actualPart.Name);
            Assert.AreEqual(2, actualPart.Price);
        }

        [Test]
        public void GetPartMethodShouldReturnNullIfPartIsIncorrect()
        {
            Computer computer = new Computer("x");
            Part part = new Part("z", 2);
            computer.AddPart(part);

            Part actualPart = computer.GetPart("q");

            Assert.IsNull(actualPart);
        }
    }
}