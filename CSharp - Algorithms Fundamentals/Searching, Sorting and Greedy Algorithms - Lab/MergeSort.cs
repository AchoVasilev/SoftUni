namespace MergeSort
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

            var sortedArr = Sort(input);
            Console.WriteLine(string.Join(' ', sortedArr));
        }

        private static int[] Sort(int[] arr)
        {
            if (arr.Length <= 1)
            {
                return arr;
            }
            
            var left = arr.Take(arr.Length / 2).ToArray();
            var right = arr.Skip(arr.Length / 2).ToArray();

            return Merge(Sort(left), Sort(right));
        }

        private static int[] Merge(int[] left, int[] right)
        {
            var merged = new int[left.Length + right.Length];

            var mergedIdx = 0;
            var leftIdx = 0;
            var rightIdx = 0;
            while (leftIdx < left.Length && rightIdx < right.Length)
            {
                if (left[leftIdx] < right[rightIdx])
                {
                    merged[mergedIdx++] = left[leftIdx++];
                }
                else
                {
                    merged[mergedIdx++] = right[rightIdx++];
                }
            }

            for (int i = leftIdx; i < left.Length; i++)
            {
                merged[mergedIdx++] = left[i];
            }

            for (int i = rightIdx; i < right.Length; i++)
            {
                merged[mergedIdx++] = right[i];
            }
            
            return merged;
        }
    }
}

