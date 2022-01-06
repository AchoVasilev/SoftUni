using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonsInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int lines = int.Parse(Console.ReadLine());
            List<Person> people = new List<Person>();

            for (int i = 0; i < lines; i++)
            {
                string[] cmdArgs = Console.ReadLine()
                    .Split(" ");
                try
                {
                    Person person = new Person(cmdArgs[0], cmdArgs[1], int.Parse(cmdArgs[2]), decimal.Parse(cmdArgs[3]));

                    people.Add(person);

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Team team = new Team("SoftUni");

            foreach (Person person in people)
            {
                team.AddPlayer(person);
            }
            decimal percentage = decimal.Parse(Console.ReadLine());
            people.ForEach(p => p.IncreaseSalary(percentage));
            people.ForEach(p => Console.WriteLine(p.ToString()));
        }
    }
}
