namespace Rumors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static Dictionary<int, List<int>> graph;
        private static Dictionary<int, int> predecessors;
        private static HashSet<int> visited;

        public static void Main(string[] args)
        {
            var numberOfPeople = int.Parse(Console.ReadLine());
            var numberOfConnections = int.Parse(Console.ReadLine());

            graph = new Dictionary<int, List<int>>();
            predecessors = new Dictionary<int, int>();
            visited = new HashSet<int>();
            
            ReadGraph(numberOfPeople, numberOfConnections);

            var x = int.Parse(Console.ReadLine());

            CalculateDistances(x);
        }

        private static void CalculateDistances(int start)
        {
            BFS(start);

            var max = graph.Keys.Max();
            for (int end = 1; end <= max; end++)
            {
                var pathLength = CalculatePathLength(start, end);
                if (pathLength <= 0 )
                {
                    continue;
                }

                Console.WriteLine($"{start} -> {end} ({pathLength})");
            }
        }

        private static int CalculatePathLength(int start, int end)
        {
            var path = new Stack<int>();
            if (!predecessors.ContainsKey(end))
            {
                return -1;
            }

            var current = end;
            while (current != start)
            {
                path.Push(current);

                current = predecessors[current];
            }

            return path.Count;
        }

        private static void BFS(int start)
        {
            predecessors[start] = 0;
            var queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var child in graph[current])
                {
                    if (visited.Contains(child))
                    {
                        continue;
                    }

                    predecessors[child] = current;
                    visited.Add(child);
                    queue.Enqueue(child);
                }

                visited.Add(current);
            }
        }

        private static void ReadGraph(int numberOfPeople, int numberOfConnections)
        {
            var vertices = Enumerable.Range(1, numberOfPeople);

            foreach (var vertex in vertices)
            {
                graph[vertex] = new List<int>();
            }

            for (int i = 0; i < numberOfConnections; i++)
            {
                var connections = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var start = connections[0];
                var end = connections[1];

                graph[start].Add(end);
                graph[end].Add(start);
            }
        }
    }
}
