namespace PathFinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static List<int>[] graph;
        private static bool[] visited;
        
        public static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            graph = new List<int>[nodes];
            ReadGraph(nodes);

            var pathsCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < pathsCount; i++)
            {
                var path = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                visited = new bool[nodes];
                var startPathIndex = 0;
                var startNode = path[startPathIndex];
                DFS(startNode, path, startPathIndex);

                if (PathExists(path))
                {
                    Console.WriteLine("yes");
                }
                else
                {
                    Console.WriteLine("no");
                }
            }
        }

        private static bool PathExists(int[] path)
        {
            foreach (var node in path)
            {
                if (!visited[node])
                {
                    return false;
                }
            }

            return true;
        }

        private static void DFS(int node, int[] path, int index)
        {
            if (visited[node] || index >= path.Length || node != path[index])
            {
                return;
            }

            visited[node] = true;
            foreach (var child in graph[node])
            {
                DFS(child, path, index + 1);
            }
        }

        private static void ReadGraph(int nodes)
        {
            for (int node = 0; node < nodes; node++)
            {
                var line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    graph[node] = new List<int>();
                }
                else
                {
                    var children = line.Split()
                        .Select(int.Parse)
                        .ToList();

                    graph[node] = children;
                }
            }
        }
    }
}
