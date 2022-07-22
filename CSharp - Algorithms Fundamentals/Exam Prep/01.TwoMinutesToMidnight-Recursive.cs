namespace TwoMinutesToMidnight
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        private static Dictionary<string, ulong> cache;
        
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());
            cache = new Dictionary<string, ulong>();
            
            var coeff = GetBinomalCoefficient(n, k);
            Console.WriteLine(coeff);
        }

        private static ulong GetBinomalCoefficient(int row, int col)
        {
            if (col == 0 || col == row)
            {
                return 1;
            }

            var key = $"{row} - {col}";
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            
            var result = GetBinomalCoefficient(row - 1, col - 1) + GetBinomalCoefficient(row - 1, col);
            cache[key] = result;

            return result;
        }
    }
}
