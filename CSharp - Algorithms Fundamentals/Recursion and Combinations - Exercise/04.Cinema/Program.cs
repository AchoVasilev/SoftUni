namespace _04.Cinema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Program
    {
        private static List<string> nonStaticPeople;
        private static string[] people;
        private static bool[] locked;

        public static void Main()
        {
            nonStaticPeople = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            people = new string[nonStaticPeople.Count];
            locked = new bool[nonStaticPeople.Count];

            while (true)
            {
                var line = Console.ReadLine();
                if (line == "generate")
                {
                    break;
                }

                var parts = line.Split(" - ", StringSplitOptions.RemoveEmptyEntries);
                var name = parts[0];
                var place = int.Parse(parts[1]) - 1;

                people[place] = name;
                locked[place] = true;

                nonStaticPeople.Remove(name);
            }

            Permute(0);
        }

        private static void Permute(int index)
        {
            if (index >= nonStaticPeople.Count)
            {
                //Print(0);
                Print();
                return;
            }

            Permute(index + 1);

            for (int i = index + 1; i < nonStaticPeople.Count; i++)
            {
                Swap(index, i);
                Permute(index + 1);
                Swap(index, i);
            }
        }

        private static void Print()
        {
            var peopleIndex = 0;
            for (int i = 0; i < people.Length; i++)
            {
                if (locked[i] == false)
                {
                    people[i] = nonStaticPeople[peopleIndex++];
                }
            }

            Console.WriteLine(string.Join(' ', people));
        }

        private static void Print(int peopleIndex)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < people.Length; i++)
            {
                if (locked[i])
                {
                    sb.Append($"{people[i]} ");
                }
                else
                {
                    sb.Append($"{nonStaticPeople[peopleIndex++]} ");
                }
            }

            Console.WriteLine(sb.ToString().Trim());
        }

        private static void Swap(int first, int second)
        {
            var temp = nonStaticPeople[first];
            nonStaticPeople[first] = nonStaticPeople[second];
            nonStaticPeople[second] = temp;
        }
    }
}
