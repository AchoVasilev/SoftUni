namespace MostReliablePath
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

        public override string ToString()
        {
            return $"{this.First} {this.Second} {this.Weight}";
        }
    }
    
    public class Program
    {
        private static List<Edge>[] graph;
        
        public static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            graph = ReadGraph(nodesCount, edgesCount);

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var distances = new double[nodesCount];
            var prev = new int[nodesCount];

            FillArrays(nodesCount, distances, prev);

            distances[source] = 100;

            var comparer = Comparer<int>.Create((first, second) => distances[second].CompareTo(distances[first]));
            var bag = new OrderedBag<int>(comparer);
            
            bag.Add(source);

            InitializeDijkstra(bag, destination, distances, prev);
            
            Console.WriteLine($"Most reliable path reliability: {distances[destination]:F2}%");
            
            var path = ReconstructPath(prev, destination);
            Console.WriteLine(string.Join(" -> ", path));
        }

        private static void InitializeDijkstra(OrderedBag<int> bag, int destination, double[] distances, int[] prev)
        {
            while (bag.Count > 0)
            {
                var node = bag.RemoveFirst();
                if (node == destination)
                {
                    break;
                }

                var children = graph[node];
                foreach (var childEdge in children)
                {
                    var child = childEdge.First == node
                        ? childEdge.Second
                        : childEdge.First;

                    if (double.IsNegativeInfinity(distances[child]))
                    {
                        bag.Add(child);
                    }

                    var newDistance = distances[node] * childEdge.Weight / 100.0;
                    if (newDistance > distances[child])
                    {
                        distances[child] = newDistance;
                        prev[child] = node;

                        bag = new OrderedBag<int>(
                            bag,
                            Comparer<int>.Create((f, s) => distances[s].CompareTo(distances[f])));
                    }
                }
            }
        }

        private static Stack<int> ReconstructPath(int[] prev, int destination)
        {
            var path = new Stack<int>();
            
            while (destination != -1)
            {
                path.Push(destination);
                destination = prev[destination];
            }

            return path;
        }

        private static void FillArrays(int nodesCount, double[] distances, int[] prev)
        {
            for (int node = 0; node < nodesCount; node++)
            {
                distances[node] = double.NegativeInfinity;
                prev[node] = -1;
            }
        }

        private static List<Edge>[] ReadGraph(int nodesCount, int edgesCount)
        {
            var result = new List<Edge>[nodesCount];
            for (int node = 0; node < nodesCount; node++)
            {
                result[node] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                var edge = new Edge()
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };
                
                result[first].Add(edge);
                result[second].Add(edge);
            }

            return result;
        }
    }
}
