namespace HelpAMole
{
    using System;

    public class Program
    {
        private static char[,] matrix;
        private static int moleRow;
        private static int moleCol;
        private static int FirstSymbolRow;
        private static int FirstSymbolCol;
        private static int SecondSymbolRow;
        private static int SecondSymbolCol;
        private static int totalPoints;

        public static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
            matrix = new char[size, size];

            ReadMatrix(size);

            while (true)
            {
                var command = Console.ReadLine();

                if (command == "End" || totalPoints >= 25)
                {
                    break;
                }

                switch (command)
                {
                    case "up":
                        Move(-1, 0);
                        break;
                    case "down":
                        Move(1, 0);
                        break;
                    case "left":
                        Move(0, -1);
                        break;
                    case "right":
                        Move(0, 1);
                        break;
                }
            }

            if (totalPoints >= 25)
            {
                Console.WriteLine("Yay! The Mole survived another game!");
                Console.WriteLine($"The Mole managed to survive with a total of {totalPoints} points.");
            }
            else
            {
                Console.WriteLine("Too bad! The Mole lost this battle!");
                Console.WriteLine($"The Mole lost the game with a total of {totalPoints} points.");
            }
            
            PrintMatrix();
        }

        private static void Move(int row, int col)
        {
            if (!IsValidRow(moleRow + row) || !IsValidCol(moleCol + col))
            {
                Console.WriteLine("Don't try to escape the playing field!");
                return;
            }

            matrix[moleRow, moleCol] = '-';
            moleRow += row;
            moleCol += col;

            if (matrix[moleRow, moleCol] == 'S')
            {
                matrix[moleRow, moleCol] = '-';

                if (moleRow == FirstSymbolRow && moleCol == FirstSymbolCol)
                {
                    moleRow = SecondSymbolRow;
                    moleCol = SecondSymbolCol;
                }
                else
                {
                    moleRow = FirstSymbolRow;
                    moleCol = FirstSymbolCol;
                }

                matrix[moleRow, moleCol] = 'M';

                totalPoints -= 3;
            }
            else if (char.IsDigit(matrix[moleRow, moleCol]))
            {
                totalPoints += (int)char.GetNumericValue(matrix[moleRow, moleCol]);
                matrix[moleRow, moleCol] = 'M';
            }
            else
            {
                matrix[moleRow, moleCol] = 'M';
            }
        }

        private static void PrintMatrix()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                }

                Console.WriteLine();
            }
        }

        private static bool IsValidRow(int row)
            => row >= 0 && row < matrix.GetLength(0);

        private static bool IsValidCol(int col)
            => col >= 0 && col < matrix.GetLength(1);

        private static void ReadMatrix(int size)
        {
            var specialSymbolsCount = 0;

            for (int i = 0; i < size; i++)
            {
                var chars = Console.ReadLine();

                for (int j = 0; j < chars.Length; j++)
                {
                    matrix[i, j] = chars[j];

                    if (matrix[i, j] == 'M')
                    {
                        moleRow = i;
                        moleCol = j;
                    }

                    if (matrix[i, j] == 'S')
                    {
                        if (specialSymbolsCount == 0)
                        {
                            FirstSymbolRow = i;
                            FirstSymbolCol = j;
                            specialSymbolsCount++;
                        }
                        else
                        {
                            SecondSymbolRow = i;
                            SecondSymbolCol = j;
                        }
                    }
                }
            }
        }
    }
}
