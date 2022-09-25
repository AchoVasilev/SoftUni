namespace CableNetwork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Edge
    {
        public int From { get; set; }
        
        public int To { get; set; }
        
        public int Weight { get; set; }
    }
    
    class Program
    {
        private static List<Edge> graph;
        private static HashSet<int> spanningTree;
        
        static void Main(string[] args)
        {
            var budget = int.Parse(Console.ReadLine());
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            ReadGraph(edgesCount);

            var parent = new int[nodesCount];
            for (int node = 0; node < parent.Length; node++)
            {
                parent[node] = node;
            }
            
            var usedBudget = Kruskal(budget, parent);
            
            Console.WriteLine($"Budget used: {usedBudget}");
        }

        private static int Kruskal(int budget, int[] parent)
        {
            var currentBudget = 0;
            foreach (var edge in graph.OrderBy(x => x.Weight))
            {
                var firstRoot = GetRoot(edge.From, parent);
                var secondRoot = GetRoot(edge.To, parent);
                
                if (firstRoot == secondRoot)
                {
                    continue;
                }

                var nonTreeNode = GetNonTreeNode(firstRoot, secondRoot);
                if (nonTreeNode == -1)
                {
                    continue;
                }
                
                if (edge.Weight > budget)
                {
                    break;
                }
                
                currentBudget += edge.Weight;
                budget -= edge.Weight;
                
                parent[firstRoot] = secondRoot;
            }

            return currentBudget;
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

        private static int GetRoot(int node, int[] parent)
        {
            
            while (node != parent[node])
            {
                node = parent[node];
            }

            return node;
        }

        private static void ReadGraph(int edgesCount)
        {
            graph = new List<Edge>();
            spanningTree = new HashSet<int>();

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
                
                graph.Add(edge);
                graph.Add(edge);
            }
        }
    }
}
