namespace LongestCommonSubsequence
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var first = Console.ReadLine();
            var second = Console.ReadLine();

            var lcs = new int[first.Length + 1, second.Length + 1];

            for (int i = 1; i < lcs.GetLength(0); i++)
            {
                for (int j = 1; j < lcs.GetLength(1); j++)
                {
                    if (first[i - 1] == second[j - 1])
                    {
                        lcs[i, j] = lcs[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        lcs[i, j] = Math.Max(lcs[i, j - 1], lcs[i - 1, j]);
                    }
                }
            }

            Console.WriteLine(lcs[first.Length, second.Length]);
        }
    }
}
