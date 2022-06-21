
using System;
namespace RecursiveDrawing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = int.Parse(Console.ReadLine());

            PrintFigure(input);
        }

        public static void PrintFigure(int number)
        {
            if (number <= 0)
            {
                return;
            }

            Console.WriteLine(new string('*', number));
            PrintFigure(number - 1);
            Console.WriteLine(new string('#', number));
        }
    }
}
