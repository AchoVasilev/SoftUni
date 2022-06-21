namespace RecursiveFactorial
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = int.Parse(System.Console.ReadLine());

            System.Console.WriteLine(CalculateFactorial(input));
        }

        static long CalculateFactorial(int number)
        {
            if (number == 0)
            {
                return 1;
            }

            return number * CalculateFactorial(number - 1);
        }
    }
}
