namespace SuperSet
{
    using System;
    using System.Linq;

    public class Program
    {
        private static int[] elements;
        private static int[] sets;
        
        public static void Main(string[] args)
        {
            elements = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            for (int i = 1; i <= elements.Length; i++)
            {
                sets = new int[i];

                GenerateCombinations(0, 0);
            }
        }

        private static void GenerateCombinations(int index, int start)
        {
            if (index >= sets.Length)
            {
                Console.WriteLine(string.Join(" ", sets));
                return;
            }

            for (int i = start; i < elements.Length; i++)
            {
                sets[index] = elements[i];
                GenerateCombinations(index + 1, i + 1);
            }
        }
    }
}
