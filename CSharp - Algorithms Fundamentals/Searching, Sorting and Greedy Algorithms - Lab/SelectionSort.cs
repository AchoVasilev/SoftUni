namespace SelectionSort
{
    using System;
    using System.Linq;
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var arr = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var result = Sort(arr);
            
            Console.WriteLine(string.Join(' ', result));
        }

        private static int[] Sort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                var min = i;

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[min])
                    {
                        min = j;
                    }
                }

                Swap(arr, i, min);
            }

            return arr;
        }

        private static void Swap(int[] arr, int i, int min)
        {
            var temp = arr[i];
            arr[i] = arr[min];
            arr[min] = temp;
        }
    }
}
