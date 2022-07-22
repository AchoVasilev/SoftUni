namespace Socks
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            var left = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var right = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var matrix = new int[left.Length + 1, right.Length + 1];
            for (int r = 1; r < matrix.GetLength(0); r++)
            {
                for (int c = 1; c < matrix.GetLength(1); c++)
                {
                    if (left[r - 1] == right[c - 1])
                    {
                        matrix[r, c] = matrix[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        matrix[r, c] = Math.Max(matrix[r - 1, c], matrix[r, c - 1]);
                    }
                }
            }

            Console.WriteLine(matrix[left.Length, right.Length]);
        }
    }
}
