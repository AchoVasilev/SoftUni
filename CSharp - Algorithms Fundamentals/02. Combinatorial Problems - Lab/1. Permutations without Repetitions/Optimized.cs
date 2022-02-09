namespace _1._Permutations_without_Repetitions
{
    using System;
    using System.Linq;

    internal class Optimized
    {
        private static string[] elements;

        public static void Main(string[] args)
        {
            var input = Console.ReadLine();

            elements = input.Split(' ').ToArray();

            Permute(0);
        }

        private static void Permute(int index)
        {
            if (index >= elements.Length)
            {
                Console.WriteLine(string.Join(' ', elements));
                return;
            }

            Permute(index + 1);
            for (int i = index + 1; i < elements.Length; i++)
            {
                Swap(index, i);
                Permute(index + 1);
                Swap(index, i);
            }
        }

        private static void Swap(int first, int second)
        {
            var temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }
    }
}
