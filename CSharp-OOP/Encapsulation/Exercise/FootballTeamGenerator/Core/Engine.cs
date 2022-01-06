using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FootballTeamGenerator.Common;
using FootballTeamGenerator.Models;

namespace FootballTeamGenerator.Core
{
    public class Engine
    {
        private readonly ICollection<Team> teams;
        public Engine()
        {
            this.teams = new List<Team>();
        }

        public void Run()
        {
            while (true)
            {
                string command = Console.ReadLine();

                if (command == "END")
                {
                    break;
                }

                string[] cmdArgs = command.Split(";");

                string cmdType = cmdArgs[0];

                try
                {
                    List<string> cmdParams = cmdArgs.Skip(1)
                        .ToList();

                    if (cmdType == "Team")
                    {
                        this.CreateTeam(cmdParams);
                    }
                    else if (cmdType == "Add")
                    {
                        this.AddPlayerToTeam(cmdParams);
                    }
                    else if (cmdType == "Remove")
                    {
                        this.RemovePlayerFromTeam(cmdParams);
                    }
                    else if (cmdType == "Rating")
                    {
                        this.RateTeam(cmdParams);
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                catch(InvalidOperationException io)
                {
                    Console.WriteLine(io.Message);
                }
            }
        }

        private void CreateTeam(IList<string> cmdArgs)
        {
            string teamName = cmdArgs[0];

            Team team = new Team(teamName);

            this.teams.Add(team);
        }

        private void AddPlayerToTeam(IList<string> cmdArgs)
        {
            string teamName = cmdArgs[0];
            string playerName = cmdArgs[1];

            this.ValidateTeamExists(teamName);

            Stats stats = this.BuildStats(cmdArgs.Skip(2).ToArray());

            Player player = new Player(playerName, stats);

            Team team = this.teams.First(t => t.Name == teamName);
            team.AddPlayer(player);
        }

        private void RemovePlayerFromTeam(IList<string> cmdArgs)
        {
            string teamName = cmdArgs[0];
            string playerName = cmdArgs[1];

            this.ValidateTeamExists(teamName);

            Team team = this.teams.First(t => t.Name == teamName);
            team.RemovePlayer(playerName);
        }

        private void RateTeam(IList<string> cmdArgs)
        {
            string teamName = cmdArgs[0];
            this.ValidateTeamExists(teamName);

            Team team = this.teams.First(t => t.Name == teamName);

            Console.WriteLine(team);
        }

        private Stats BuildStats(string[] stats)
        {
            int endurance = int.Parse(stats[0]);
            int sprint = int.Parse(stats[1]);
            int dribble = int.Parse(stats[2]);
            int passing = int.Parse(stats[3]);
            int shooting = int.Parse(stats[4]);

            Stats stat = new Stats(endurance, sprint, dribble, passing, shooting);

            return stat;
        }

        private void ValidateTeamExists(string teamName)
        {
            if (!this.teams.Any(t => t.Name == teamName))
            {
                throw new InvalidOperationException(String.Format(GlobalConstants.MissingTeamExcMsg, teamName));
            }
        }
    }
}
