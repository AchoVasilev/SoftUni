using System;
using System.Linq;

namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] numbers = Console.ReadLine()
                .Split(" ");

            foreach (var number in numbers)
            {
                ICallable phone;

                if (!number.All(char.IsDigit))
                {
                    Console.WriteLine("Invalid number!");
                    continue;
                }

                if (number.Length == 10)
                {
                    phone = new SmartPhone();
                }
                else
                {
                    phone = new StationaryPhone();
                }

                phone.Call(number);
            }

            string[] urls = Console.ReadLine()
                .Split(" ");

            foreach (var url in urls)
            {
                if (url.Any(char.IsDigit))
                {
                    Console.WriteLine("Invalid URL!");
                    continue;
                }

                IBrowsable smartPhone = new SmartPhone();
                smartPhone.Browse(url);
            }
        }
    }
}
