namespace Universes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static Dictionary<string, List<string>> universe;
        private static HashSet<string> visited;
        private static HashSet<string> cycles;
        
        public static void Main(string[] args)
        {
            universe = new Dictionary<string, List<string>>();
            visited = new HashSet<string>();
            cycles = new HashSet<string>();
            
            var planetsCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < planetsCount; i++)
            {
                var planets = Console.ReadLine().Split(" - ", StringSplitOptions.RemoveEmptyEntries);
                var start = planets[0];
                var end = planets[1];

                if (!universe.ContainsKey(start))
                {
                    universe[start] = new List<string>();
                }

                if (!universe.ContainsKey(end))
                {
                    universe[end] = new List<string>();
                }

                universe[start].Add(end);
                universe[end].Add(start);
            }

            var count = 0;
            foreach (var node in universe.Keys)
            {
                if (visited.Contains(node))
                {
                    continue;
                }

                count++;
                VisitUniverse(node);
            }

            Console.WriteLine(count);
        }

        private static void VisitUniverse(string node)
        {
            var queue = new Queue<string>();
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                visited.Add(current);

                var notVisitedSystems = universe[current]
                    .Where(s => !visited.Contains(s));
                foreach (var system in notVisitedSystems)
                {
                    queue.Enqueue(system);
                }
            }
        }
    }
}
