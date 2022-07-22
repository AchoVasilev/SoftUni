namespace Time
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        //int[][] outperforms int[,]
        private static int[][] table;
        public static void Main(string[] args)
        {
            var firstSequence = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var secondSequence = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            InitializeTable(firstSequence, secondSequence);
            var longestCommonSubsequence = new Stack<int>();

            var row = firstSequence.Length;
            var col = secondSequence.Length;

            while (row > 0 && col > 0)
            {
                if (firstSequence[row - 1] == secondSequence[col - 1])
                {
                    longestCommonSubsequence.Push(firstSequence[row - 1]);
                    row--;
                    col--;
                }
                else if(table[row][col - 1] >= table[row - 1][col])
                {
                    col--;
                }
                else
                {
                    row--;
                }
            }

            var lcsLength = table[^1][^1];
            Console.WriteLine(string.Join(' ', longestCommonSubsequence));
            Console.WriteLine(lcsLength);
        }

        private static void InitializeTable(int[] firstSequence, int[] secondSequence)
        {
            table = new int[firstSequence.Length + 1][];
            for (int row = 0; row < table.Length; row++)
            {
                table[row] = new int[secondSequence.Length + 1];
            }

            for (int row = 1; row < table.Length; row++)
            {
                for (int col = 1; col < table[row].Length; col++)
                {
                    if (firstSequence[row - 1] == secondSequence[col - 1])
                    {
                        table[row][col] = table[row - 1][col - 1] + 1;
                    }
                    else
                    {
                        table[row][col] = Math.Max(table[row - 1][col], table[row][col - 1]);
                    }
                }
            }
        }
    }
}
