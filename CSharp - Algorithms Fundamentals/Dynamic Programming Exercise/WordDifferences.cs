namespace WordDifferences
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var firstStr = Console.ReadLine();
            var secondStr = Console.ReadLine();

            var dp = new int[firstStr.Length + 1, secondStr.Length + 1];
            for (int row = 1; row < dp.GetLength(0); row++)
            {
                dp[row, 0] = row;
            }

            for (int col = 1; col < dp.GetLength(1); col++)
            {
                dp[0, col] = col;
            }

            for (int row = 1; row < dp.GetLength(0); row++)
            {
                for (int col = 1; col < dp.GetLength(1); col++)
                {
                    if (firstStr[row - 1] == secondStr[col - 1])
                    {
                        dp[row, col] = dp[row - 1, col - 1];
                    }
                    else
                    {
                        dp[row, col] = Math.Min(dp[row - 1, col], dp[row, col - 1]) + 1;
                    }
                }
            }

            var res = dp[firstStr.Length, secondStr.Length];
            Console.WriteLine($"Deletions and Insertions: {res}");
        }
    }
}
