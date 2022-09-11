namespace PrimAlgorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public static class Program
    {
        private static Dictionary<int, List<Edge>> graph;
        private static HashSet<int> forestNodes;
        private static List<Edge> forestEdges;

        public static void Main(string[] args)
        {
            graph = new Dictionary<int, List<Edge>>();
            forestNodes = new HashSet<int>();
            forestEdges = new List<Edge>();

            var edges = int.Parse(Console.ReadLine());
            ReadGraph(edges);

            foreach (var graphKey in graph.Keys)
            {
                if (!forestNodes.Contains(graphKey))
                {
                    Prim(graphKey);
                }
            }

            foreach (var forestEdge in forestEdges)
            {
                Console.WriteLine($"{forestEdge.First} - {forestEdge.Second}");
            }
        }

        private static void Prim(int startingNode)
        {
            forestNodes.Add(startingNode);

            var bag = new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            bag.AddMany(graph[startingNode]);

            while (bag.Count > 0)
            {
                var minEdge = bag.RemoveFirst();
                var nonTreeNode = GetNonTreeNode(minEdge);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                forestNodes.Add(nonTreeNode);
                forestEdges.Add(minEdge);
                bag.AddMany(graph[nonTreeNode]);
            }
        }

        private static int GetNonTreeNode(Edge minEdge)
        {
            var nonTreeNode = -1;

            if (forestNodes.Contains(minEdge.First) && !forestNodes.Contains(minEdge.Second))
            {
                nonTreeNode = minEdge.Second;
            }

            if (forestNodes.Contains(minEdge.Second) && !forestNodes.Contains(minEdge.First))
            {
                nonTreeNode = minEdge.First;
            }

            return nonTreeNode;
        }

        private static void ReadGraph(int edges)
        {
            for (int i = 0; i < edges; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                if (!graph.ContainsKey(first))
                {
                    graph[first] = new List<Edge>();
                }

                if (!graph.ContainsKey(second))
                {
                    graph[second] = new List<Edge>();
                }

                var edge = new Edge()
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };

                graph[first].Add(edge);
                graph[second].Add(edge);
            }
        }
    }
}
