namespace LongestPath
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        private static List<Edge>[] graph;
        
        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());
            
            graph = new List<Edge>[nodes + 1];

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<Edge>();
            }

            ReadGraph(edgesCount);

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var sortedNodes = TopologicalSorting();

            var distances = new double[graph.Length];
            Array.Fill(distances, double.NegativeInfinity);

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
                    }
                }
            }

            Console.WriteLine(distances[destination]);
        }

        private static Stack<int> TopologicalSorting()
        {
            var visited = new bool[graph.Length];
            var stack = new Stack<int>();

            for (int node = 1; node < graph.Length; node++)
            {
                DFS(node, visited, stack);
            }

            return stack;
        }

        private static void DFS(int node, bool[] visited, Stack<int> stack)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var edge in graph[node])
            {
                DFS(edge.To, visited, stack);
            }
            
            stack.Push(node);
        }

        private static void ReadGraph(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(' ')
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

    class Edge
    {
        public int From { get; set; }
        
        public int To { get; set; }
        
        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{this.From} {this.To} {this.Weight}";
        }
    }
}
