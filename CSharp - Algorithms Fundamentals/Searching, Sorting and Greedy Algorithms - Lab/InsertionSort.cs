namespace InsertionSort
{
    using System;
    using System.Linq;
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var inputArr = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var result = Sort(inputArr);
            
            Console.WriteLine(string.Join(' ', result));
        }

        private static int[] Sort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                var j = i;

                while (j > 0 && arr[j] < arr[j - 1])
                {
                    Swap(arr, j, j - 1);

                    j--;
                }
            }

            return arr;
        }

        private static void Swap(int[] arr, int idx, int idxToSwap)
        {
            var temp = arr[idx];
            arr[idx] = arr[idxToSwap];
            arr[idxToSwap] = temp;
        }
    }
}

