using System;
using System.Collections.Generic;

namespace P05ComparingObjects
{
    class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                string[] tokens = input.Split();
                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                string town = tokens[2];

                people.Add(new Person(name, age, town));
            }

            int index = int.Parse(Console.ReadLine()) - 1;

            Person person = people[index];

            int equalCounter = 0;
            int nonEqualCounter = 0;

            foreach (var man in people)
            {
                if (man.CompareTo(person) == 0)
                {
                    equalCounter++;
                }
                else
                {
                    nonEqualCounter++;
                }
            }

            if (equalCounter > 1)
            {
                Console.WriteLine($"{equalCounter} {nonEqualCounter} {equalCounter + nonEqualCounter}");
            }
            else
            {
                Console.WriteLine("No matches");
            }
        }
    }
}
