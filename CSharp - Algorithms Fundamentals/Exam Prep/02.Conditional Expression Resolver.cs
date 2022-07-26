namespace Second
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var result = Resolve(input);

            Console.WriteLine(result);
        }

        private static string Resolve(string input)
        {
            var tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            var stack = new Stack<string>();
            for (var i = tokens.Length - 1; i >= 0; i--)
            {
                var current = tokens[i];
                if (stack.Count != 0 && stack.Peek() == "?")
                {
                    stack.Pop();
                    var first = stack.Pop();

                    stack.Pop();
                    var second = stack.Pop();
                    
                    stack.Push(current == "?" ? first : second);
                }
                else
                {
                    stack.Push(current);
                }
            }

            return stack.Peek();
        }
    }
}
