namespace BigTrip
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static List<Edge>[] graph;
        public static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            ReadGraph(nodesCount, edgesCount);

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var sortedNodes = TopologicalSorting();

            var distances = new double[graph.Length];
            
            var prev = new int[graph.Length];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = double.NegativeInfinity;
                prev[i] = -1;
            }
            
            distances[source] = 0;

            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                foreach (var edge in graph[node])
                {
                    var newDistance = distances[node] + edge.Weight;

                    if (newDistance > distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                    }
                }
            }

            Console.WriteLine(distances[destination]);

            var path = ReconstructPath(prev, destination);
            Console.WriteLine(string.Join(" ", path));
        }

        private static Stack<int> ReconstructPath(int[] prev, int destination)
        {
            var path = new Stack<int>();
            var node = destination;

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static Stack<int> TopologicalSorting()
        {
            var result = new Stack<int>();
            var visited = new bool[graph.Length];

            for (int node = 1; node < graph.Length; node++)
            {
                DFS(node, result, visited);
            }

            return result;
        }

        private static void DFS(int node, Stack<int> result, bool[] visited)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var edge in graph[node])
            {
                DFS(edge.To, result, visited);
            }
            
            result.Push(node);
        }

        private static void ReadGraph(int nodesCount, int edgesCount)
        {
            graph = new List<Edge>[nodesCount + 1];
            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeData[0];
                var to = edgeData[1];
                var weight = edgeData[2];
                
                graph[from].Add(new Edge()
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }
        }
    }
}
