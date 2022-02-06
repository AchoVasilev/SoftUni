namespace _04._Recursive_Factorial
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = int.Parse(Console.ReadLine());

            var result = GetFactorial(input);
            Console.WriteLine(result);
        }

        private static long GetFactorial(int num)
        {
            if (num <= 0)
            {
                return 1;
            }

            return num * GetFactorial(num - 1);
        }
    }
}