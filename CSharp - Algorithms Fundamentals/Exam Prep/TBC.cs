namespace TBC
{
    using System;

    public class Program
    {
        private static char[,] matrix;
        private const char VisitedSymbol = 'v';
        private const char DirtSymbol = 'd';
        
        public static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());
            matrix = new char[rows, cols];

            FillMatrix(rows);

            var tunnels = 0;
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (IsVisited(row, col) || IsDirt(row, col))
                    {
                        continue;
                    }
                    
                    ExploreTunnel(row, col);
                    tunnels++;
                }
            }

            Console.WriteLine(tunnels);
        }

        private static void ExploreTunnel(int row, int col)
        {
            if (IsOutside(row, col) || IsVisited(row, col) || IsDirt(row, col))
            {
                return;
            }

            matrix[row, col] = VisitedSymbol;
            
            ExploreTunnel(row - 1, col);
            ExploreTunnel(row + 1, col);
            ExploreTunnel(row, col - 1);
            ExploreTunnel(row, col + 1);
            ExploreTunnel(row - 1, col - 1);
            ExploreTunnel(row - 1, col + 1);
            ExploreTunnel(row + 1, col - 1);
            ExploreTunnel(row + 1, col + 1);
        }

        private static void FillMatrix(int rows)
        {
            for (int row = 0; row < rows; row++)
            {
                var rowElements = Console.ReadLine();

                for (int col = 0; col < rowElements.Length; col++)
                {
                    matrix[row, col] = rowElements[col];
                }
            }
        }

        private static bool IsOutside(int row, int col)
        {
            return row < 0 || row >= matrix.GetLength(0) || col < 0 || col >= matrix.GetLength(1);
        }
        
        private static bool IsVisited(int row, int col)
        {
            return matrix[row, col] == VisitedSymbol;
        }

        private static bool IsDirt(int row, int col)
        {
            return matrix[row, col] == DirtSymbol;
        }
    }
}
