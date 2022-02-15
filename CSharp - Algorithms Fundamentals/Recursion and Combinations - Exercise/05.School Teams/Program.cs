class Program
{
    public static void Main()
    {
        var girls = Console.ReadLine().Split(", ");
        var girlsComb = new string[3];
        var girlsCombs = new List<string[]>();
        GenerateCombinations(0, 0, girls, girlsComb, girlsCombs);

        var boys = Console.ReadLine().Split(", ");
        var boysComb = new string[2];
        var boysCombs = new List<string[]>();

        GenerateCombinations(0, 0, boys, boysComb, boysCombs);

        PrintCombinations(girlsCombs, boysCombs);
    }

    private static void PrintCombinations(List<string[]> girlsCombs, List<string[]> boysCombs)
    {
        foreach (var girlComb in girlsCombs)
        {
            foreach (var boyComb in boysCombs)
            {
                Console.WriteLine($"{string.Join(", ", girlComb)}, {string.Join(", ", boyComb)}");
            }
        }
    }

    private static void GenerateCombinations(int index, int start, string[] elements, string[] combination, List<string[]> combinations)
    {
        if (index >= combination.Length)
        {
            combinations.Add(combination.ToArray());
            return;
        }

        for (int i = start; i < elements.Length; i++)
        {
            combination[index] = elements[i];
            GenerateCombinations(index + 1, i + 1, elements, combination, combinations);
        }
    }
}