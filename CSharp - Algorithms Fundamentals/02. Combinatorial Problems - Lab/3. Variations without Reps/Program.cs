class Program
{
    private static int k;
    private static string[] elements;
    private static string[] variations;
    private static bool[] used;

    public static void Main(string[] args)
    {
        elements = Console.ReadLine().Split(' ');
        k = int.Parse(Console.ReadLine());

        used = new bool[elements.Length];
        variations = new string[k];

        Variations(0);
    }

    private static void Variations(int index)
    {
        if (index >= variations.Length)
        {
            Console.WriteLine(string.Join(' ', variations));
            return;
        }

        for (int i = 0; i < elements.Length; i++)
        {
            if (used[i] == false)
            {
                used[i] = true;
                variations[index] = elements[i];
                Variations(index + 1);

                used[i] = false;
            }
        }
    }
}