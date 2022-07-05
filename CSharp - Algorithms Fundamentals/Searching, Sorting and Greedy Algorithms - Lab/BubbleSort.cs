namespace BubbleSort
{
    using System;
    using System.Linq;
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var result = Sort(input);
            
            Console.WriteLine(string.Join(' ', result));
        }

        private static int[] Sort(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 1; j < input.Length; j++)
                {
                    if (input[j - 1] > input[j])
                    {
                        Swap(input, j - 1, j);
                    }
                }
            }

            return input;
        }

        private static void Swap(int[] input, int i, int j)
        {
            var temp = input[i];
            input[i] = input[j];
            input[j] = temp;
        }
    }
}
