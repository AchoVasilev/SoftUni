using System;

namespace Basketball
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Team
    {
        private List<Player> players;

        public Team(string name, int openPositions, char group)
        {
            this.Name = name;
            this.OpenPositions = openPositions;
            this.Group = group;
            this.players = new List<Player>();
        }
        
        public string Name { get; set; }
        
        public int OpenPositions { get; set; }
        
        public char Group { get; set; }

        public int Count => this.players.Count;

        public string AddPlayer(Player player)
        {
            if (string.IsNullOrEmpty(player.Name) || string.IsNullOrEmpty(player.Position))
            {
                return "Invalid player's information.";
            }

            if (this.OpenPositions == 0)
            {
                return "There are no more open positions.";
            }

            if (player.Rating < 80)
            {
                return "Invalid player's rating.";
            }
            
            this.players.Add(player);
            this.OpenPositions--;

            return $"Successfully added {player.Name} to the team. Remaining open positions: {this.OpenPositions}.";
        }

        public bool RemovePlayer(string name)
        {
            var player = this.players.FirstOrDefault(x => x.Name == name);

            if (player is null)
            {
                return false;
            }

            this.players.Remove(player);
            this.OpenPositions++;
            
            return true;
        }

        public int RemovePlayerByPosition(string position)
        {
            var removedPlayers = this.players.RemoveAll(x => x.Position == position);
            if (removedPlayers > 0)
            {
                this.OpenPositions += removedPlayers;
            }

            return removedPlayers;
        }

        public Player RetirePlayer(string name)
        {
            var player = this.players.FirstOrDefault(x => x.Name == name);
            if (player is null)
            {
                return null;
            }

            player.Retired = true;

            return player;
        }

        public List<Player> AwardPlayer(int games)
        {
            var playersToAward = this.players.Where(x => x.Games >= games)
                .ToList();

            return playersToAward;
        }

        public string Report()
        {
            var sb = new StringBuilder();
            var activePlayers = this.players.Where(x => x.Retired == false)
                .ToList();

            sb.AppendLine($"Active players competing for Team {this.Name} from Group {this.Group}:");
            foreach (var activePlayer in activePlayers)
            {
                sb.AppendLine(activePlayer.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
