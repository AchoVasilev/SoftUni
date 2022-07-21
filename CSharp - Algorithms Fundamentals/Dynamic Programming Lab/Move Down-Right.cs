namespace MoveDownRight
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var matrix = new int[rows, cols];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var rowElements = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                for (int j = 0; j < rowElements.Length; j++)
                {
                    matrix[i, j] = rowElements[j];
                }
            }

            var dp = new int[rows, cols];
            dp[0, 0] = matrix[0, 0];

            for (int c = 1; c < cols; c++)
            {
                dp[0, c] = dp[0, c - 1] + matrix[0, c];
            }

            for (int r = 1; r < rows; r++)
            {
                dp[r, 0] = dp[r - 1, 0] + matrix[r, 0];
            }

            for (int r = 1; r < rows; r++)
            {
                for (int c = 1; c < cols; c++)
                {
                    var upper = dp[r - 1, c];
                    var left = dp[r, c - 1];

                    dp[r, c] = Math.Max(upper, left) + matrix[r, c];
                }
            }

            var path = new Stack<string>();
            var row = rows - 1;
            var col = cols - 1;
            while (row > 0 && col > 0)
            {
                path.Push($"[{row}, {col}]");
                var upper = dp[row - 1, col];
                var left = dp[row, col - 1];

                if (upper > left)
                {
                    row -= 1;
                }
                else
                {
                    col -= 1;
                }
            }

            while (row > 0)
            {
                path.Push($"[{row}, {col}]");
                row--;
            }

            while (col > 0)
            {
                path.Push($"[{row}, {col}]");
                col--;
            }

            path.Push($"[{row}, {col}]");
            Console.WriteLine(string.Join(" ", path));
        }
    }
}
