class Program
{
    public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var k = int.Parse(Console.ReadLine());

        Console.WriteLine(Binom(n, k));
    }

    private static int Binom(int row, int col)
    {
        if (row <= 1 || col == 0 || col == row)
        {
            return 1;
        }

        return Binom(row - 1, col) + Binom(row - 1, col - 1);
    }
}