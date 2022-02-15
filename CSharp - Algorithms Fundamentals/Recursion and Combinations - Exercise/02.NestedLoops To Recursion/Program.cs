class Program
{
    public static void Main()
    {
        var num = int.Parse(Console.ReadLine());
        var arr = new int[num];

        GenerateVector(0, arr);
    }

    private static void GenerateVector(int cell, int[] arr)
    {
        if (cell >= arr.Length)
        {
            Console.WriteLine(string.Join(' ', arr));
            return;
        }

        for (int i = 1; i <= arr.Length; i++)
        {
            arr[cell] = i;
            GenerateVector(cell + 1, arr);
        }
    }
}