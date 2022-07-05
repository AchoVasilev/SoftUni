namespace BinarySearch
{
    using System;
    using System.Linq;
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ')
                .Select(int.Parse)
                .ToArray();

            var searchedNum = int.Parse(Console.ReadLine());

            var result = BinarySearch(input, searchedNum);
        
            Console.WriteLine(result);
        }

        public static int BinarySearch(int[] arr, int searchedNumber)
        {
            var left = 0;
            var right = arr.Length - 1;

            while (left <= right)
            {
                var mid = (left + right) / 2;
                var element = arr[mid];

                if (element == searchedNumber)
                {
                    return mid;
                }

                if (searchedNumber > element)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        
            return -1;
        }
    }
}
