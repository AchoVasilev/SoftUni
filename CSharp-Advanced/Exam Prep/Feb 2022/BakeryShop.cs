class Program
{
    public static void Main()
    {
        var water = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(double.Parse)
            .ToArray();

        var flour = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(double.Parse)
            .ToArray();

        var bakedProducts = new Dictionary<string, int>();
        var waterQueue = new Queue<double>(water);
        var flourStack = new Stack<double>(flour);

        while (waterQueue.Any() && flourStack.Any())
        {
            var currentWater = waterQueue.Peek();
            var currentFlour = flourStack.Peek();
            var currentRatio = currentWater + currentFlour;

            var waterPercent = (currentWater * 100) / currentRatio;
            var flourPercent = (currentFlour * 100) / currentRatio;

            if (waterPercent == 50 && flourPercent == 50)
            {
                if (!bakedProducts.ContainsKey("Croissant"))
                {
                    bakedProducts["Croissant"] = 0;
                }

                bakedProducts["Croissant"]++;
                waterQueue.Dequeue();
                flourStack.Pop();
            }
            else if (waterPercent == 40 && flourPercent == 60)
            {
                if (!bakedProducts.ContainsKey("Muffin"))
                {
                    bakedProducts["Muffin"] = 0;
                }

                bakedProducts["Muffin"]++;
                waterQueue.Dequeue();
                flourStack.Pop();
            }
            else if (waterPercent == 30 && flourPercent == 70)
            {
                if (!bakedProducts.ContainsKey("Baguette"))
                {
                    bakedProducts["Baguette"] = 0;
                }

                bakedProducts["Baguette"]++;
                waterQueue.Dequeue();
                flourStack.Pop();
            }
            else if(waterPercent == 20 && flourPercent == 80)
            {
                if (!bakedProducts.ContainsKey("Bagel"))
                {
                    bakedProducts["Bagel"] = 0;
                }

                bakedProducts["Bagel"]++;
                waterQueue.Dequeue();
                flourStack.Pop();
            }
            else
            {
                var flourToTake = waterQueue.Dequeue();
                var flourToReturn = flourStack.Pop() - flourToTake;

                flourStack.Push(flourToReturn);
                if (!bakedProducts.ContainsKey("Croissant"))
                {
                    bakedProducts["Croissant"] = 0;
                }

                bakedProducts["Croissant"]++;
            }
        }

        var sortedProducts = bakedProducts.OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key);

        foreach (var product in sortedProducts)
        {
            Console.WriteLine($"{product.Key}: {product.Value}");
        }

        if (waterQueue.Count == 0)
        {
            Console.WriteLine("Water left: None");
        }
        else
        {
            Console.WriteLine($"Water left: {string.Join(", ", waterQueue)}");
        }

        if (flourStack.Count == 0)
        {
            Console.WriteLine("Flour left: None");
        }
        else
        {
            Console.WriteLine($"Flour left: {string.Join(", ", flourStack)}");
        }
    }
}
