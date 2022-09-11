namespace KruskalAlgorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Edge
    {
        public int First { get; set; }
        
        public int Second { get; set; }
        
        public int Weight { get; set; }
    }
    
    public class Program
    {
        private static List<Edge> edges;
        private static List<Edge> forest;
        private static int maxNode = -1;
        private static int[] parent;
        
        public static void Main(string[] args)
        {
            edges = new List<Edge>();
            forest = new List<Edge>();
            
            var edgesCount = int.Parse(Console.ReadLine());
            ReadEdges(edgesCount);
            
            parent = new int[maxNode + 1];
            for (int node = 0; node < parent.Length; node++)
            {
                parent[node] = node;
            }
            
            var sortedEdges = edges
                .OrderBy(e => e.Weight)
                .ToArray();

            TraverseSortedEdgesAndFillForest(sortedEdges);

            foreach (var edge in forest)
            {
                Console.WriteLine($"{edge.First} - {edge.Second}");
            }
        }

        private static void TraverseSortedEdgesAndFillForest(Edge[] sortedEdges)
        {
            foreach (var sortedEdge in sortedEdges)
            {
                var firstNodeRoot = FindRoot(sortedEdge.First);
                var secondNodeRoot = FindRoot(sortedEdge.Second);

                if (firstNodeRoot == secondNodeRoot)
                {
                    continue;
                }

                parent[firstNodeRoot] = secondNodeRoot;

                forest.Add(sortedEdge);
            }
        }

        private static int FindRoot(int node)
        {
            while (node != parent[node])
            {
                node = parent[node];
            }

            return node;
        }

        private static void ReadEdges(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgesArgs = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var first = edgesArgs[0];
                var second = edgesArgs[1];
                var weight = edgesArgs[2];

                if (first > maxNode)
                {
                    maxNode = first;
                }

                if (second > maxNode)
                {
                    maxNode = second;
                }
                
                edges.Add(new Edge()
                {
                    First = first,
                    Second = second,
                    Weight = weight
                });
            }
        }
    }
}

