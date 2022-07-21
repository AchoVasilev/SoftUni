namespace Fibonacci
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        private static Dictionary<int, long> cache;
        public static void Main(string[] args)
        {
            cache = new Dictionary<int, long>();
            var num = int.Parse(Console.ReadLine());

            var result = CalculateFib(num);

            Console.WriteLine(result);
        }

        private static long CalculateFib(int num)
        {
            if (cache.ContainsKey(num))
            {
                return cache[num];
            }

            if (num < 2)
            {
                return num;
            }

            var result = CalculateFib(num - 1) + CalculateFib(num - 2);

            cache[num] = result;
            
            return result;
        }
    }
}
