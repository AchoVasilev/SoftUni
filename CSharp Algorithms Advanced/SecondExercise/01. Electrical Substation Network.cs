namespace ElectricalSubstationNetwork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        private static List<int>[] originalGraph;
        private static List<int>[] reversedGraph;
        private static Stack<int> sorted;
        
        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var linesCount = int.Parse(Console.ReadLine());

            ReadGraph(nodesCount, linesCount);

            sorted = TopologicalSorting();
            var visited = new bool[nodesCount];

            while (sorted.Count > 0)
            {
                var node = sorted.Pop();

                if (visited[node])
                {
                    continue;
                }

                var component = new Stack<int>();
                
                DFS(node, visited, component, reversedGraph);

                Console.WriteLine($"{string.Join(", ", component)}");
            }
        }

        private static Stack<int> TopologicalSorting()
        {
            var stack = new Stack<int>();
            var visited = new bool[originalGraph.Length];

            for (int node = 0; node < originalGraph.Length; node++)
            {
                DFS(node, visited, stack, originalGraph);
            }

            return stack;
        }

        private static void DFS(int node, bool[] visited, Stack<int> stack, List<int>[] graph)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                DFS(child, visited, stack, graph);
            }
            
            stack.Push(node);
        }

        private static void ReadGraph(int nodesCount, int linesCount)
        {
            originalGraph = new List<int>[nodesCount];
            reversedGraph = new List<int>[nodesCount];

            for (int node = 0; node < nodesCount; node++)
            {
                originalGraph[node] = new List<int>();
                reversedGraph[node] = new List<int>();
            }

            for (int i = 0; i < linesCount; i++)
            {
                var data = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var node = data[0];

                for (int j = 1; j < data.Length; j++)
                {
                    var child = data[j];
                    
                    originalGraph[node].Add(child);
                    reversedGraph[child].Add(node);
                }
            }
        }
    }
}
