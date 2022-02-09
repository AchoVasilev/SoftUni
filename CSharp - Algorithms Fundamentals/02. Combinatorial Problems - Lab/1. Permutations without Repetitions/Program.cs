class Program
{
    private static string[] elements;
    private static string[] permutations;
    private static bool[] used;

    public static void Main(string[] args)
    {
        var input = Console.ReadLine();

        elements = input.Split(' ').ToArray();
        permutations = new string[elements.Length];
        used = new bool[elements.Length];

        Permute(0);
    }

    private static void Permute(int index)
    {
        if (index >= permutations.Length)
        {
            Console.WriteLine(string.Join(' ', permutations));
            return;
        }

        for (int i = 0; i < elements.Length; i++)
        {
            if (used[i] == false)
            {
                used[i] = true;
                permutations[index] = elements[i];

                Permute(index + 1);

                used[i] = false;
            }
        }
    }
}