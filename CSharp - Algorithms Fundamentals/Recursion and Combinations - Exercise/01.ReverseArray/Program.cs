class Program
{
    public static void Main()
    {
        var elements = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Reverse(elements, 0);

        Console.WriteLine(string.Join(' ', elements));
    }

    private static void Reverse(string[] elements, int index)
    {
        if (index == elements.Length / 2)
        {
            return;
        }

        var temp = elements[index];
        elements[index] = elements[elements.Length - index - 1];
        elements[elements.Length - index - 1] = temp;

        Reverse(elements, index + 1);
    }
}