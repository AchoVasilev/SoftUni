namespace AreasInMatrix
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        private static char[,] graph;
        private static bool[,] visited;
        private static IDictionary<char, int> areas;
        
        public static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            areas = new SortedDictionary<char, int>();
            graph = new char[rows, cols];
            visited = new bool[rows, cols];
            
            for (int row = 0; row < rows; row++)
            {
                var rowElements = Console.ReadLine();
                for (int col = 0; col < cols; col++)
                {
                    graph[row, col] = rowElements[col];
                }
            }

            var areasCount = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (visited[row, col])
                    {
                        continue;
                    }

                    var nodeValue = graph[row, col];
                    DFS(row, col, nodeValue);

                    areasCount++;
                    
                    if (!areas.ContainsKey(nodeValue))
                    {
                        areas[nodeValue] = 0;
                    }

                    areas[nodeValue]++;
                }
            }

            Console.WriteLine($"Areas: {areasCount}");
            foreach (var kvp in areas)
            {
                Console.WriteLine($"Letter '{kvp.Key}' -> {kvp.Value}");
            }
        }

        private static void DFS(int row, int col, char parentNode)
        {
            if (row < 0 || row >= graph.GetLength(0) || col < 0 || col >= graph.GetLength(1))
            {
                return;
            }
            
            if (visited[row, col])
            {
                return;
            }

            if (graph[row, col] != parentNode)
            {
                return;
            }

            visited[row, col] = true;
            
            DFS(row, col - 1, parentNode);
            DFS(row, col + 1,parentNode);
            DFS(row - 1, col, parentNode);
            DFS(row + 1, col,parentNode);
        }
    }
}
