namespace BaristaContest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var coffeQtys = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            
            var milkQtys = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var coffeeQueue = new Queue<int>(coffeQtys);
            var milkStack = new Stack<int>(milkQtys);

            var drinksTemplate = new Dictionary<int, string>()
            {
                {50,"Cortado"},
                {75, "Espresso"},
                {100, "Capuccino"},
                {150, "Americano"},
                {200, "Latte"}
            };

            var totalDrinks = new Dictionary<string, int>();

            while (coffeeQueue.Any() && milkStack.Any())
            {
                var firstCoffee = coffeeQueue.Peek();
                var lastMilk = milkStack.Peek();
                var sum = firstCoffee + lastMilk;
                
                if (!drinksTemplate.ContainsKey(sum))
                {
                    coffeeQueue.Dequeue();
                    var milk = milkStack.Pop() - 5;
                    
                    milkStack.Push(milk);
                }
                else if (drinksTemplate.ContainsKey(sum))
                {
                    if (!totalDrinks.ContainsKey(drinksTemplate[sum]))
                    {
                        totalDrinks[drinksTemplate[sum]] = 0;
                    }

                    totalDrinks[drinksTemplate[sum]]++;

                    coffeeQueue.Dequeue();
                    milkStack.Pop();
                }
            }

            var sb = new StringBuilder();
            if (coffeeQueue.Count == 0 && milkStack.Count == 0)
            {
                sb.AppendLine("Nina is going to win! She used all the coffee and milk!");
            }
            else
            {
                sb.AppendLine("Nina needs to exercise more! She didn't use all the coffee and milk!");
            }

            if (coffeeQueue.Count == 0)
            {
                sb.AppendLine("Coffee left: none");
            }
            else
            {
                sb.AppendLine($"Coffee left: {string.Join(", ", coffeeQueue)}");
            }
            
            if (milkStack.Count == 0)
            {
                sb.AppendLine("Milk left: none");
            }
            else
            {
                sb.AppendLine($"Milk left: {string.Join(", ", milkStack)}");
            }

            var sortedResult = totalDrinks.OrderBy(x => x.Value)
                .ThenByDescending(x => x.Key);

            foreach (var kvp in sortedResult)
            {
                sb.AppendLine($"{kvp.Key}: {kvp.Value}");
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}
