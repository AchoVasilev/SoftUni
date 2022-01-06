using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CounterStrike.Models.Maps
{
    public class Map : IMap
    {
        public string Start(ICollection<IPlayer> players)
        {
            var terrorist = players.Where(t => t.GetType() == typeof(Terrorist)).ToList();
            var counterTerrorist = players.Where(ct => ct.GetType() == typeof(CounterTerrorist)).ToList();

            while (terrorist.Any(t => t.IsAlive) && counterTerrorist.Any(ct => ct.IsAlive))
            {
                foreach (var terr in terrorist)
                {
                    if (!terr.IsAlive)
                    {
                        continue;
                    }

                    foreach (var counterTerr in counterTerrorist)
                    {
                        if (!counterTerr.IsAlive)
                        {
                            continue;
                        }

                        counterTerr.TakeDamage(terr.Gun.Fire());
                    }
                }

                foreach (var counterTerr in counterTerrorist)
                {
                    if (!counterTerr.IsAlive)
                    {
                        continue;
                    }

                    foreach (var terr in terrorist)
                    {
                        if (!terr.IsAlive)
                        {
                            continue;
                        }

                        terr.TakeDamage(counterTerr.Gun.Fire());
                    }
                }
            }

            string result = string.Empty;

            if (terrorist.Any(t => t.IsAlive))
            {
                result = "Terrorist wins!";
            }
            else
            {
                result = "Counter Terrorist wins!";
            }

            return result;
        }
    }
}
