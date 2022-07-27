namespace MoveDownRight
{
    using System;

    public class Program
    {
        private static long[][] matrix;
        public static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            matrix = new long[rows][];
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = new long[cols];
            }

            for (int c = 1; c < cols; c++)
            {
                matrix[0][c] = 1;
            }
            
            for (int r = 1; r < rows; r++)
            {
                matrix[r][0] = 1;
            }

            for (int r = 1; r < rows; r++)
            {
                for (int c = 1; c < cols; c++)
                {
                    var upper = matrix[r - 1][c];
                    var left = matrix[r][c - 1];

                    matrix[r][c] = upper + left;
                }
            }

            Console.WriteLine(matrix[rows - 1][cols - 1]);
        }
    }
}
