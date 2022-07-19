namespace CyclesInGraph
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        private static Dictionary<string, List<string>> graph;
        private static HashSet<string> visited;
        private static HashSet<string> cycles;

        public static void Main(string[] args)
        {
            graph = new Dictionary<string, List<string>>();
            visited = new HashSet<string>();
            cycles = new HashSet<string>();

            while (true)
            {
                var edge = Console.ReadLine();

                if (edge == "End")
                {
                    break;
                }

                var tokens = edge.Split('-');
                var from = tokens[0];
                var destination = tokens[1];

                if (!graph.ContainsKey(from))
                {
                    graph[from] = new List<string>();
                }


                if (!graph.ContainsKey(destination))
                {
                    graph[destination] = new List<string>();
                }

                graph[from].Add(destination);
            }

            try
            {
                foreach (var node in graph.Keys)
                {
                    DFS(node);
                }

                Console.WriteLine("Acyclic: Yes");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Acyclic: No");
            }
        }

        private static void DFS(string node)
        {
            if (cycles.Contains(node))
            {
                throw new InvalidOperationException();
            }
            
            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);
            cycles.Add(node);
            
            foreach (var child in graph[node])
            {
                DFS(child);
            }

            cycles.Remove(node);
        }
    }
}
