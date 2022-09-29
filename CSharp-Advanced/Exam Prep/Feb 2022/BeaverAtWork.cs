class Program
{
    private static int beaverRow = 0;
    private static int beaverCol = 0;
    private static int totalBranches = 0;

    public static void Main()
    {
        var size = int.Parse(Console.ReadLine());

        var matrix = new char[size, size];
        var branches = new List<char>();

        for (int i = 0; i < size; i++)
        {
            var chars = Console.ReadLine()
                .Replace(" ", string.Empty)
                .ToCharArray();

            for (int j = 0; j < chars.Length; j++)
            {
                matrix[i, j] = chars[j];

                if (chars[j] == 'B')
                {
                    beaverRow = i;
                    beaverCol = j;
                }
                else if (char.IsLower(chars[j]))
                {
                    totalBranches++;
                }
            }
        }

        var lastDirection = string.Empty;
        while (true)
        {
            var input = Console.ReadLine();
            if (input == "end")
            {
                break;
            }

            lastDirection = input;
            switch (input)
            {
                case "up":
                    Move(matrix, -1, 0, branches, lastDirection);
                    break;
                case "down":
                    Move(matrix, 1, 0, branches, lastDirection);
                    break;
                case "left":
                    Move(matrix, 0, -1, branches, lastDirection);
                    break;
                case "right":
                    Move(matrix, 0, 1, branches, lastDirection);
                    break;
                default:
                    break;
            }

            if (totalBranches == 0)
            {
                break;
            }
        }

        if (totalBranches > 0)
        {
            Console.WriteLine($"The Beaver failed to collect every wood branch. There are {totalBranches} branches left.");
        }
        else
        {
            Console.WriteLine($"The Beaver successfully collect {branches.Count} wood branches: {string.Join(", ", branches)}.");
        }

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j]);
                Console.Write(' ');
            }

            Console.WriteLine();
        }
    }

    private static void Move(char[,] matrix, int row, int col, List<char> branches, string lastDirection)
    {
        if (IsValidRow(beaverRow + row, matrix) == false || IsValidCol(beaverCol + col, matrix) == false)
        {
            if (branches.Any())
            {
                branches.RemoveAt(branches.Count - 1);
            }

            return;
        }

        matrix[beaverRow, beaverCol] = '-';
        beaverRow += row;
        beaverCol += col;

        if (char.IsLower(matrix[beaverRow, beaverCol]))
        {
            branches.Add(matrix[beaverRow, beaverCol]);
            totalBranches--;
            matrix[beaverRow, beaverCol] = 'B';
        }
        else if (matrix[beaverRow, beaverCol] == 'F')
        {
            matrix[beaverRow, beaverCol] = '-';

            switch (lastDirection)
            {
                case "up":
                    if (beaverRow == 0)
                    {
                        if (char.IsLower(matrix[beaverRow, beaverCol]))
                        {
                            branches.Add(matrix[beaverRow, beaverCol]);
                            totalBranches--;
                        }

                        beaverRow = matrix.GetLength(0) - 1;
                        matrix[beaverRow, beaverCol] = 'B';
                    }
                    else
                    {
                        if (char.IsLower(matrix[beaverRow, beaverCol]))
                        {
                            branches.Add(matrix[beaverRow, beaverCol]);
                            totalBranches--;
                        }

                        beaverRow = 0;
                        matrix[beaverRow, beaverCol] = 'B';
                    }
                    break;
                case "down":
                    if (beaverRow == matrix.GetLength(0) - 1)
                    {
                        if (char.IsLower(matrix[beaverRow, beaverCol]))
                        {
                            branches.Add(matrix[beaverRow, beaverCol]);
                            totalBranches--;
                        }

                        beaverRow = 0;
                        matrix[beaverRow, beaverCol] = 'B';
                    }
                    else
                    {
                        if (char.IsLower(matrix[matrix.GetLength(0) - 1, col]))
                        {
                            branches.Add(matrix[matrix.GetLength(0) - 1, col]);
                            totalBranches--;
                        }

                        beaverRow = matrix.GetLength(0) - 1;
                        matrix[beaverRow, beaverCol] = 'B';
                    }
                    break;
                case "left":
                    if (beaverCol == 0)
                    {
                        if (char.IsLower(matrix[beaverRow, beaverCol]))
                        {
                            branches.Add(matrix[beaverRow, beaverCol]);
                            totalBranches--;
                        }

                        beaverCol = matrix.GetLength(1) - 1;
                        matrix[beaverRow, beaverCol] = 'B';
                    }
                    else
                    {
                        if (char.IsLower(matrix[row, matrix.GetLength(1) - 1]))
                        {
                            branches.Add(matrix[row, matrix.GetLength(1) - 1]);
                            totalBranches--;
                        }

                        beaverCol = 0;
                        matrix[beaverRow, beaverCol] = 'B';
                    }
                    break;
                case "right":
                    if (beaverCol == matrix.GetLength(1) - 1)
                    {
                        if (char.IsLower(matrix[beaverRow, beaverCol]))
                        {
                            branches.Add(matrix[beaverRow, beaverCol]);
                            totalBranches--;
                        }

                        beaverCol = 0;
                        matrix[beaverRow, beaverCol] = 'B';
                    }
                    else
                    {
                        if (char.IsLower(matrix[beaverRow, beaverCol]))
                        {
                            branches.Add(matrix[beaverRow, beaverCol]);
                            totalBranches--;
                        }

                        beaverCol = matrix.GetLength(1) - 1;
                        matrix[beaverRow, beaverCol] = 'B';
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            matrix[beaverRow, beaverCol] = 'B';
        }
    }

    private static bool IsValidRow(int index, char[,] matrix)
        => index >= 0 && index < matrix.GetLength(0);

    private static bool IsValidCol(int index, char[,] matrix)
        => index >= 0 && index < matrix.GetLength(1);
}
