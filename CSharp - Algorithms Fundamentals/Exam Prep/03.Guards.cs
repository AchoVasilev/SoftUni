namespace Third
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static Dictionary<int, List<int>> graph;
        private static List<int> visited;

        public static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            graph = new Dictionary<int, List<int>>(nodes);
            visited = new List<int>();

            ReadGraph(nodes, edges);

            var startNode = int.Parse(Console.ReadLine());

            DFS(startNode);

            var result = graph.Select(x => x.Key)
                .Where(x => !visited.Contains(x))
                .ToList();
            
            Console.WriteLine(string.Join(" ", result));
        }

        private static void DFS(int startNode)
        {
            if (visited.Contains(startNode))
            {
                return;
            }

            visited.Add(startNode);
            if (!graph.ContainsKey(startNode))
            {
                return;
            }

            foreach (var child in graph[startNode])
            {
                DFS(child);
            }
        }

        private static void Bfs(int vertex)
        {
            var queue = new Queue<int>();
            queue.Enqueue(vertex);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                visited.Add(vertex);

                var children = graph[node];

                foreach (var child in children)
                {
                    if (visited.Contains(child))
                    {
                        continue;
                    }

                    queue.Enqueue(child);
                    visited.Add(child);
                }
            }
        }

        private static void ReadGraph(int nodes, int edges)
        {
            for (int node = 1; node <= nodes; node++)
            {
                graph[node] = new List<int>();
            }

            for (int edge = 0; edge < edges; edge++)
            {
                var line = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                var from = line[0];
                var to = line[1];

                if (!graph.ContainsKey(from))
                {
                    graph[from] = new List<int>();
                }

                graph[from].Add(to);
            }
        }
    }
}
