namespace FindAllPathsInLabyrinth
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var matrix = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                var chars = Console.ReadLine();

                for (int j = 0; j < chars.Length; j++)
                {
                    matrix[i, j] = chars[j];
                }
            }

            FindPaths(matrix, 0, 0, new List<string>(), string.Empty);
        }

        private static void FindPaths(char[,] matrix, int row, int col, List<string> path, string direction)
        {
            if (row < 0 || row >= matrix.GetLength(0) || col < 0 || col >= matrix.GetLength(1))
            {
                return;
            }

            if (matrix[row, col] == 'v' || matrix[row, col] == '*')
            {
                return;
            }

            path.Add(direction);

            if (matrix[row, col] == 'e')
            {
                Console.WriteLine(string.Join(string.Empty, path));
                path.RemoveAt(path.Count - 1);
                return;
            }

            matrix[row, col] = 'v';

            FindPaths(matrix, row - 1, col, path, "U"); //UP
            FindPaths(matrix, row + 1, col, path, "D"); //DOWN
            FindPaths(matrix, row, col - 1, path, "L"); //LEFT
            FindPaths(matrix, row, col + 1, path, "R"); //RIGHT

            matrix[row, col] = '-';
            path.RemoveAt(path.Count - 1);
        }
    }
}
