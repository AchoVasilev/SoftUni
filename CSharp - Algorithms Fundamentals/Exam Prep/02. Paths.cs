namespace Paths
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static Dictionary<int, List<int>> graph;
        
        public static void Main(string[] args)
        {
            var numberOfNodes = int.Parse(Console.ReadLine());
            graph = new Dictionary<int, List<int>>();

            for (int i = 0; i < numberOfNodes - 1; i++)
            {
                graph[i] = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
            }

            GeneratePaths(numberOfNodes);
        }

        private static void GeneratePaths(int numberOfNodes)
        {
            for (int i = 0; i < numberOfNodes - 1; i++)
            {
                GetPaths(i, numberOfNodes - 1);
            }
        }

        private static void GetPaths(int start, int end)
        {
            var visited = new bool[end + 1];
            var pathList = new List<int>() { start };

            PathsGenerator(start, end, visited, pathList);
        }

        private static void PathsGenerator(int current, int destination, bool[] visited, List<int> pathList)
        {
            if (current == destination)
            {
                Console.WriteLine(string.Join(" ", pathList));
                return;
            }

            visited[current] = true;

            foreach (var child in graph[current])
            {
                if (visited[child])
                {
                    continue;
                }
                
                pathList.Add(child);
                PathsGenerator(child, destination, visited, pathList);
                pathList.Remove(child);
            }

            visited[current] = false;
        }
    }
}
