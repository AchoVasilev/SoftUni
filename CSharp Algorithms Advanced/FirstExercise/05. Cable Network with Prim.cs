namespace CableNetwork
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    class Edge
    {
        public int From { get; set; }
        
        public int To { get; set; }
        
        public int Weight { get; set; }
    }
    
    class Program
    {
        private static List<Edge>[] graph;
        private static HashSet<int> spanningTree;
        
        static void Main(string[] args)
        {
            var budget = int.Parse(Console.ReadLine());
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            ReadGraph(nodesCount, edgesCount);

            var usedBudget = Prim(budget);
            
            Console.WriteLine($"Budget used: {usedBudget}");
        }

        private static int Prim(int budget)
        {
            var usedBudget = 0;

            var bag = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight.CompareTo(s.Weight)));

            foreach (var node in spanningTree)
            {
                bag.AddMany(graph[node]);    
            }

            while (bag.Count > 0)
            {
                var edge = bag.RemoveFirst();

                var nonTreeNode = GetNonTreeNode(edge.From, edge.To);
                if (nonTreeNode == -1)
                {
                    continue;
                }

                if (edge.Weight > budget)
                {
                    break;
                }

                usedBudget += edge.Weight;
                budget -= edge.Weight;

                spanningTree.Add(nonTreeNode);
                bag.AddMany(graph[nonTreeNode]);
            }
            
            return usedBudget;
        }

        private static int GetNonTreeNode(int edgeFrom, int edgeTo)
        {
            var nonTreeNode = -1;

            if (spanningTree.Contains(edgeFrom) && !spanningTree.Contains(edgeTo))
            {
                nonTreeNode = edgeTo;
            }
            else if (spanningTree.Contains(edgeTo) && !spanningTree.Contains(edgeFrom))
            {
                nonTreeNode = edgeFrom;
            }

            return nonTreeNode;
        }

        private static void ReadGraph(int nodesCount, int edgesCount)
        {
            graph = new List<Edge>[nodesCount];
            spanningTree = new HashSet<int>();

            for (int node = 0; node < nodesCount; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split();

                var first = int.Parse(edgeData[0]);
                var second = int.Parse(edgeData[1]);
                var weight = int.Parse(edgeData[2]);

                if (edgeData.Length == 4)
                {
                    spanningTree.Add(first);
                    spanningTree.Add(second);
                }

                var edge = new Edge()
                {
                    From = first,
                    To = second,
                    Weight = weight
                };
                
                graph[first].Add(edge);
                graph[second].Add(edge);
            }
        }
    }
}
