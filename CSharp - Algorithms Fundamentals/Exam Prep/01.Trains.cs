namespace First
{
    using System;
    using System.Linq;

    public class Program
    {
        private static decimal[] arrivalSequence;
        private static decimal[] departureSequence;
        
        public static void Main(string[] args)
        {
             arrivalSequence = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(decimal.Parse)
                .ToArray();

             departureSequence = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(decimal.Parse)
                .ToArray();

             var result = CalculatePlatforms();

             Console.WriteLine(result);
        }

        private static int CalculatePlatforms()
        {
            Array.Sort(arrivalSequence);
            Array.Sort(departureSequence);

            var platforms = 1;
            var result = 1;

            var arrivalIndex = 1;
            var departureIndex = 0;

            var count = arrivalSequence.Length;

            while (arrivalIndex < count && departureIndex < count)
            {
                if (arrivalSequence[arrivalIndex] < departureSequence[departureIndex])
                {
                    platforms++;
                    arrivalIndex++;
                }
                else if (arrivalSequence[arrivalIndex] >= departureSequence[departureIndex])
                {
                    platforms--;
                    departureIndex++;
                }

                if (platforms > result)
                {
                    result = platforms;
                }
            }

            return result;
        }
    }
}
