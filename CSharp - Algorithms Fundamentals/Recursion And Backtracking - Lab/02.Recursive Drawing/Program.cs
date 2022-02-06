namespace _02.Recursive_Drawing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = int.Parse(Console.ReadLine());
            PrintFigure(input);
        }

        private static void PrintFigure(int input)
        {
            if (input <= 0)
            {
                return;
            }

            Console.WriteLine(new string('*', input));
            PrintFigure(input - 1);
            Console.WriteLine(new string('#', input));
        }
    }
}