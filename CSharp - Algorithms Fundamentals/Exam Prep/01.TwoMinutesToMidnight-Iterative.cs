namespace TwoMinutesToMidnight
{
    using System;
    using System.Numerics;

    public class Program
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            var result = GetFactorial(n) / (GetFactorial(k) * GetFactorial(n - k));

            Console.WriteLine(result);
        }

        private static BigInteger GetFactorial(int num)
        {
            BigInteger factorial = 1;
            for (int i = 2; i <= num; i++)
            {
                factorial *= i;
            }

            return factorial;
        }
    }
}
