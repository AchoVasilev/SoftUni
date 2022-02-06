namespace Lab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var result = GetSum(input, 0);
        }

        private static int GetSum(int[] numbers, int index)
        {
            if (index >= numbers.Length)
            {
                return 0;
            }

            return numbers[index] + GetSum(numbers, index + 1);
        }
    }
}