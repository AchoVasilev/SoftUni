namespace _03.Generating_0_1_Vectors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = int.Parse(Console.ReadLine());
            var array = new int[input];

            PrintNumbers(0, array);
        }

        private static void PrintNumbers(int index, int[] array)
        {
            if (index >= array.Length)
            {
                Console.WriteLine(string.Join(string.Empty, array));
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                array[index] = i;
                PrintNumbers(index + 1, array);
            }
        }
    }
}