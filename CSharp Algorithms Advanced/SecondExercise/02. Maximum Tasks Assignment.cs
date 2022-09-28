namespace MaximumTasksAssignment
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    class Program
    {
        private static int[,] graph;
        private static int[] parents;
        
        static void Main(string[] args)
        {
            var peopleCount = int.Parse(Console.ReadLine());
            var tasksCount = int.Parse(Console.ReadLine());

            var start = 0;
            var nodes = peopleCount + tasksCount + 2;
            var target = nodes - 1;

            ConstructGraph(peopleCount, tasksCount, nodes, start, target);

            parents = new int[graph.GetLength(0)];
            Array.Fill(parents, -1);

            while (BFS(start, target))
            {
                var node = target;

                while (node != start)
                {
                    var parent = parents[node];

                    graph[parent, node] = 0;
                    graph[node, parent] = 1;

                    node = parent;
                }
            }

            for (int person = 1; person <= peopleCount; person++)
            {
                for (int task = peopleCount + 1; task <= peopleCount + tasksCount; task++)
                {
                    if (graph[task, person] > 0)
                    {
                        Console.WriteLine($"{ (char)(64 + person) }-{task - peopleCount}");
                    }
                }
            }
        }

        private static bool BFS(int start, int target)
        {
            var visited = new bool[graph.GetLength(0)];
            var queue = new Queue<int>();

            visited[start] = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == target)
                {
                    return true;
                }

                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (!visited[child] && graph[node, child] > 0)
                    {
                        parents[child] = node;
                        visited[child] = true;
                        queue.Enqueue(child);
                    }
                }
            }

            return false;
        }

        private static void ConstructGraph(int peopleCount, int tasksCount, int nodes, int start, int target)
        {
            graph = new int[nodes, nodes];
            for (int person = 1; person <= peopleCount; person++)
            {
                graph[start, person] = 1;
            }

            for (int task = peopleCount + 1; task <= peopleCount + tasksCount; task++)
            {
                graph[task, target] = 1;
            }

            for (int person = 1; person <= peopleCount; person++)
            {
                var personCapabilities = Console.ReadLine();

                for (int task = 0; task < personCapabilities.Length; task++)
                {
                    if (personCapabilities[task] == 'Y')
                    {
                        graph[person, peopleCount + 1 + task] = 1;
                    }
                }
            }
        }
    }
}
