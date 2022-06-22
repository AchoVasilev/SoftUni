using System;
namespace GeneratingVectors
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrLength = int.Parse(Console.ReadLine());

            var arr = new int[arrLength];

            PrintNumbers(0, arr);
        }

        private static void PrintNumbers(int index, int[] arr)
        {
            if (index >= arr.Length)
            {
                Console.WriteLine(string.Join(string.Empty, arr));

                return;
            }

            for (int i = 0; i <= 1; i++)
            {
                arr[index] = i;

                PrintNumbers(index + 1, arr);
            }
        }
    }
}
