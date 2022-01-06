using System;
using System.Collections.Generic;

using MilitaryElite.Contracts;
using MilitaryElite.Models;

namespace MilitaryElite
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Dictionary<string, Private> privates = new Dictionary<string, Private>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "End")
                {
                    break;
                }

                string[] cmdArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string soldierType = cmdArgs[0];
                string id;
                string firstName;
                string lastName;
                decimal salary;
                Corps corps;

                switch (soldierType)
                {
                    case "Private":
                        id = cmdArgs[1];
                        firstName = cmdArgs[2];
                        lastName = cmdArgs[3];
                        salary = decimal.Parse(cmdArgs[4]);

                        Private privateSoldier = new Private(id, firstName, lastName, salary);
                        privates.Add(id, privateSoldier);
                        Console.WriteLine(privateSoldier);
                        break;
                    case "LieutenantGeneral":
                        id = cmdArgs[1];
                        firstName = cmdArgs[2];
                        lastName = cmdArgs[3];
                        salary = decimal.Parse(cmdArgs[4]);

                        LeutenantGeneral leutenantGeneral = new LeutenantGeneral(id, firstName, lastName, salary);

                        if (cmdArgs.Length >= 5)
                        {
                            for (int i = 5; i < cmdArgs.Length; i++)
                            {
                                string privateId = cmdArgs[i];
                                privateSoldier = privates[privateId];

                                leutenantGeneral.Privates.Add(privateSoldier);
                            }
                        }

                        Console.WriteLine(leutenantGeneral);
                        break;
                    case "Engineer":
                        id = cmdArgs[1];
                        firstName = cmdArgs[2];
                        lastName = cmdArgs[3];
                        salary = decimal.Parse(cmdArgs[4]);

                        if (Enum.TryParse(cmdArgs[5], out corps))
                        {
                            Engineer engineer = new Engineer(id, firstName, lastName, salary, corps);

                            if (cmdArgs.Length >= 6)
                            {
                                for (int i = 6; i < cmdArgs.Length; i += 2)
                                {
                                    string repairPartName = cmdArgs[i];
                                    int repairHours = int.Parse(cmdArgs[i + 1]);

                                    Repair repair = new Repair(repairPartName, repairHours);

                                    engineer.Repair.Add(repair);
                                }
                            }

                            Console.WriteLine(engineer);
                        }
                        break;
                    case "Commando":
                        id = cmdArgs[1];
                        firstName = cmdArgs[2];
                        lastName = cmdArgs[3];
                        salary = decimal.Parse(cmdArgs[4]);

                        if (Enum.TryParse(cmdArgs[5], out corps))
                        {
                            Commando commando = new Commando(id, firstName, lastName, salary, corps);

                            if (cmdArgs.Length >= 6)
                            {
                                for (int i = 6; i < cmdArgs.Length; i += 2)
                                {
                                    if (Enum.TryParse(cmdArgs[i + 1], out MissionState missionState))
                                    {
                                        string missionName = cmdArgs[i];
                                        Mission mission = new Mission(missionName, missionState);
                                        commando.Mission.Add(mission);
                                    }
                                }

                                Console.WriteLine(commando);
                            }
                        }
                        break;
                    case "Spy":
                        id = cmdArgs[1];
                        firstName = cmdArgs[2];
                        lastName = cmdArgs[3];
                        int codeNumber = int.Parse(cmdArgs[4]);

                        Spy spy = new Spy(id, firstName, lastName, codeNumber);
                        Console.WriteLine(spy);
                        break;
                    default:
                        throw new ArgumentException("Invalid Type of Soldier!");
                }
            }
        }
    }
}
