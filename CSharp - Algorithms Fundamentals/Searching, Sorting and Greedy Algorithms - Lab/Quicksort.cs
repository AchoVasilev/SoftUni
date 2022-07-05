namespace QuickSort
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

            QuickSort(inputArr, 0, inputArr.Length - 1);
            
            Console.WriteLine(string.Join(' ', inputArr));
        }

        private static void QuickSort(int[] arr, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            var pivot = start;
            var left = start + 1;
            var right = end;

            while (left <= right)
            {
                if (arr[left] > arr[pivot] && arr[pivot] > arr[right])
                {
                    Swap(arr, left, right);
                }

                if (arr[left] <= arr[pivot])
                {
                    left++;
                }

                if (arr[right] >= arr[pivot])
                {
                    right--;
                }
            }
            
            Swap(arr, pivot, right);
            QuickSort(arr, start, right - 1);
            QuickSort(arr, right + 1, end);
        }

        private static void Swap(int[] arr, int left, int right)
        {
            var temp = arr[left];
            arr[left] = arr[right];
            arr[right] = temp;
        }
    }
}

