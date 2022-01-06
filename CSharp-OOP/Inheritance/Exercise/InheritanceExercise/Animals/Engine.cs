using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animals
{
    class Engine
    {
        private readonly List<Animal> animals;

        public Engine()
        {
            animals = new List<Animal>();
        }

        public void Run()
        {
            while (true)
            {
                string type = Console.ReadLine();

                if (type == "Beast!")
                {
                    break;
                }

                string[] animalArgs = Console.ReadLine()
                    .Split(" ")
                    .ToArray();

                Animal animal;

                try
                {
                    animal = GetAnimal(type, animalArgs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }

                this.animals.Add(animal);
            }

            foreach (var animal in this.animals)
            {
                Console.WriteLine(animal);
            }
        }
        private Animal GetAnimal(string type, string[] animalArgs)
        {
            string name = animalArgs[0];
            int age = int.Parse(animalArgs[1]);
            string gender = null;
            Animal animal = null;

            if (animalArgs.Length >= 3)
            {
                gender = animalArgs[2];
            }

            switch (type)
            {
                case "Dog":
                    animal = new Dog(name, age, gender);
                    break;
                case "Cat":
                    animal = new Cat(name, age, gender);
                    break;
                case "Frog":
                    animal = new Frog(name, age, gender);
                    break;
                case "Kitten":
                    animal = new Kitten(name, age);
                    break;
                case "Tomcat":
                    animal = new TomCat(name, age);
                    break;
                default:
                    throw new ArgumentException("Invalid input!");
            }

            return animal;
        }
    }
}
