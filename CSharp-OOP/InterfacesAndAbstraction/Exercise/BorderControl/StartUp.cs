using System;
using System.Linq;
using System.Collections.Generic;

namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Citizen> citizens = new List<Citizen>();
            List<Robot> robots = new List<Robot>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "End")
                {
                    break;
                }

                string[] inputArgs = input.Split(" ");


                if (inputArgs.Length == 3)
                {
                    string name = inputArgs[0];
                    int age = int.Parse(inputArgs[1]);
                    string id = inputArgs[2];

                    Citizen citizen = new Citizen(name, age, id);
                    citizens.Add(citizen);
                }
                else if (inputArgs.Length == 2)
                {
                    string model = inputArgs[0];
                    string id = inputArgs[1];

                    Robot robot = new Robot(model, id);
                    robots.Add(robot);
                }
            }

            string fakeId = Console.ReadLine();

            citizens.Where(a => a.Id.EndsWith(fakeId))
                .Select(a => a.Id)
                .ToList()
                .ForEach(Console.WriteLine);

            robots.Where(a => a.Id.EndsWith(fakeId))
                .Select(a => a.Id)
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
