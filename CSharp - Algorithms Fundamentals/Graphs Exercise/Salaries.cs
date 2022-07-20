namespace Salaries
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        private static List<int>[] graph;
        private static Dictionary<int, int> visited;
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            graph = new List<int>[n];
            visited = new Dictionary<int, int>();
            
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();

                var nodeChildren = Console.ReadLine();

                for (int j = 0; j < nodeChildren.Length; j++)
                {
                    if (nodeChildren[j] == 'Y')
                    {
                        graph[i].Add(j);
                    }
                }
            }

            var salary = 0;
            for (int node = 0; node < graph.Length; node++)
            {
                salary += DFS(node);
            }

            Console.WriteLine(salary);
        }

        private static int DFS(int node)
        {
            if (visited.ContainsKey(node))
            {
                return visited[node];
            }
            
            var salary = 0;
            if (graph[node].Count == 0)
            {
                salary = 1;
            }
            else
            {
                foreach (var child in graph[node])
                {
                    salary += DFS(child);
                }
            }

            visited[node] = salary;
            
            return salary;
        }
    }
}
