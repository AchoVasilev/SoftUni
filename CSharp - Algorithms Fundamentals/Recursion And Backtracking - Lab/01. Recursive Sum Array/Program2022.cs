namespace RecursiveArraySum
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Console.WriteLine(GetSum(input, 0));
        }

        private static int GetSum(int[] arr, int idx)
        {
            if (idx >= arr.Length)
            {
                return 0;
            }

            return arr[idx] + GetSum(arr, idx + 1);
        }
    }
}
