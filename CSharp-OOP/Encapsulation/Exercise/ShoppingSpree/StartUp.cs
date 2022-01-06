using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();

            string[] peopleInputArgs = Console.ReadLine()
                .Split(";", StringSplitOptions.RemoveEmptyEntries);

            foreach (var argument in peopleInputArgs)
            {
                string[] tokens = argument.Split("=");
                string name = tokens[0];
                decimal money = decimal.Parse(tokens[1]);

                try
                {
                    people.Add(new Person(name, money));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            string[] productInputArgs = Console.ReadLine()
                .Split(";", StringSplitOptions.RemoveEmptyEntries);

            foreach (var argument in productInputArgs)
            {
                string[] tokens = argument.Split("=");
                string name = tokens[0];
                decimal cost = decimal.Parse(tokens[1]);

                try
                {
                    products.Add(new Product(name, cost));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                string[] inputArgs = input.Split(" ");
                string personName = inputArgs[0];
                string productName = inputArgs[1];

                Person person = people.Where(p => p.Name == personName)
                    .First();
                Product product = products.Where(p => p.Name == productName)
                    .First();

                if (person.Money >= product.Cost)
                {
                    person.Money -= product.Cost;
                    person.AddProduct(product);

                    Console.WriteLine($"{person.Name} bought {product.Name}");
                }
                else
                {
                    Console.WriteLine($"{person.Name} can't afford {product.Name}");
                }
            }

            foreach (var person in people)
            {
                if (person.SeeBag().Count > 0)
                {
                    Console.WriteLine("{0} - {1}", person.Name, string.Join(", ", person.SeeBag().Select(p => p.Name)));
                }
                else
                {
                    Console.WriteLine("{0} - Nothing bought", person.Name);
                }
            }
        }
    }
}
