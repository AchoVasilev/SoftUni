namespace CheapTownTour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Edge
    {
        public int First { get; set; }    
        
        public int Second { get; set; }

        public int Weight { get; set; }
    }
    
    class Program
    {
        private static List<Edge> graph = new List<Edge>();
        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            ReadEdges(edgesCount);

            var root = new int[nodesCount];
            for (int node = 0; node < root.Length; node++)
            {
                root[node] = node;
            }

            var totalCost = 0;

            foreach (var edge in graph.OrderBy(x => x.Weight))
            {
                var firstRoot = GetRoot(edge.First, root);
                var secondRoot = GetRoot(edge.Second, root);

                if (firstRoot != secondRoot)
                {
                    root[firstRoot] = secondRoot;
                    totalCost += edge.Weight;
                }
            }

            Console.WriteLine($"Total cost: {totalCost}");
        }

        private static int GetRoot(int node, int[] root)
        {
            while (node != root[node])
            {
                node = root[node];
            }

            return node;
        }

        private static void ReadEdges(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(" - ")
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];
                
                graph.Add(new Edge()
                {
                    First = first,
                    Second = second,
                    Weight = weight
                });
            }
        }
    }
}
