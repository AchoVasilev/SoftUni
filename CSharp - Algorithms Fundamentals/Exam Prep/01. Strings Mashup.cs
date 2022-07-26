namespace StringsMashup
{
    using System;
    using System.Linq;

    public class Program
    {
        private static char[] elements;
        private static char[] slots;
        public static void Main(string[] args)
        {
            var str = Console.ReadLine();
            var maxLength = int.Parse(Console.ReadLine());

            elements = str.ToCharArray()
                .OrderBy(x => x)
                .ToArray();

            slots = new char[maxLength];

            Combinations(0, 0);
        }

        private static void Combinations(int index, int start)
        {
            if (index >= slots.Length)
            {
                Console.WriteLine(string.Join(string.Empty, slots));
                return;
            }

            for (int i = start; i < elements.Length; i++)
            {
                slots[index] = elements[i];
                Combinations(index + 1, i);
            }
        }
    }
}
