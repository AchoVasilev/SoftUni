using System;
using System.Linq;

namespace P04Froggy
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int[] stones = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            Lake lake = new Lake(stones);

            Console.WriteLine(string.Join(", ", lake));
        }
    }
}
