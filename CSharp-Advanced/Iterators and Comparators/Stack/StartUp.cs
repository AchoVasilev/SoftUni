using System;
using System.Linq;

namespace P03Stack
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "END")
                {
                    break;
                }

                string[] tokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                switch (tokens[0])
                {
                    case "Push":
                        int[] elements = tokens.Skip(1)
                            .Select(e => e.Split(',').First())
                            .Select(int.Parse)
                            .ToArray();

                        stack.Push(elements);
                        break;

                    case "Pop":
                        try
                        {
                            stack.Pop();
                        }
                        catch (InvalidOperationException ioe)
                        {
                            Console.WriteLine(ioe.Message);
                        }
                        break;
                }
            }

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
        }
    }
}
