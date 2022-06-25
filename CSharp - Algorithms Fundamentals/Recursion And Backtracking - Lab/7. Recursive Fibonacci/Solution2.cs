namespace RecursiveFib
{
    using System;
    using System.Collections.Generic;
    public class Program
    {
        private static Dictionary<long, long> numbers = new Dictionary<long, long>();

        public static void Main(string[] args)
        {
            var input = int.Parse(Console.ReadLine());

            var result = CalcFib(input);
            Console.WriteLine(result);
        }

        private static long CalcFib(int num)
        {
            if (num <= 1)
            {
                return 1;
            }

            if (numbers.ContainsKey(num))
            {
                return numbers[num];
            }

            var sum = CalcFib(num - 1) + CalcFib(num - 2);
            numbers[num] = sum;

            return sum;
        }
    }
}
