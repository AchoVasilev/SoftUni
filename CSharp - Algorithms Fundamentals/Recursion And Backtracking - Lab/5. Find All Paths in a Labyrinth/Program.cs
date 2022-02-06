namespace _5._Find_All_Paths_in_a_Labyrinth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var labyrinth = new char[rows, cols];
            for (int r = 0; r < rows; r++)
            {
                var columns = Console.ReadLine();
                for (int c = 0; c < columns.Length; c++)
                {
                    labyrinth[r, c] = columns[c];
                }
            }

            FindAllPaths(labyrinth, 0, 0, new List<string>(), string.Empty);
        }

        private static void FindAllPaths(char[,] labyrinth, int row, int col, List<string> directions, string direction)
        {
            if (row < 0 || row >= labyrinth.GetLength(0) || col < 0 || col >= labyrinth.GetLength(1))
            {
                return;
            }

            if (labyrinth[row, col] == '*' || labyrinth[row, col] == 'v')
            {
                return;
            }

            directions.Add(direction);

            if (labyrinth[row, col] == 'e')
            {
                Console.WriteLine(string.Join(string.Empty, directions));
                directions.RemoveAt(directions.Count - 1);

                return;
            }

            //Visited cell
            labyrinth[row, col] = 'v';

            FindAllPaths(labyrinth, row - 1, col, directions, "U"); //UP
            FindAllPaths(labyrinth, row + 1, col, directions, "D"); //DOWN
            FindAllPaths(labyrinth, row, col - 1, directions, "L"); //LEFT
            FindAllPaths(labyrinth, row, col + 1, directions, "R"); //RIGHT

            labyrinth[row, col] = '-';
            directions.RemoveAt(directions.Count - 1);
        }
    }
}