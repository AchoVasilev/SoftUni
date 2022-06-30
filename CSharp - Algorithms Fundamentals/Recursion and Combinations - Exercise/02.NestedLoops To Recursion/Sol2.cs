using System;

namespace NestedLoopsToRecursion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());
            var arr = new int[num];

            GenerateVector(arr, 0);
        }

        private static void GenerateVector(int[] arr, int cell)
        {
            if (cell >= arr.Length)
            {
                Console.WriteLine(string.Join(' ', arr));
                return;
            }

            for (int i = 1; i <= arr.Length; i++)
            {
                arr[cell] = i;

                GenerateVector(arr, cell + 1);
            }
        }
    }
}
