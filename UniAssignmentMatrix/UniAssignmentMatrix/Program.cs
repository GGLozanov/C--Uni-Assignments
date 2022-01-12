using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UniAssignmentMatrix
{
    internal class Program
    {
        private static void PrintMatrix(decimal[,] matrix)
        {
            var row = matrix.GetLength(0);
            var col = matrix.GetLength(1);

            for (var i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write(matrix[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        private static bool CheckIdentity(decimal[,] matrix)
        {
            var row = matrix.GetLength(0);
            var col = matrix.GetLength(1);
            
            for (var i = 0; i < row * col; i++)
            {
                var currentRow = i % col;
                var currentColumn = i / col;

                if (currentRow == currentColumn)
                {
                    if (matrix[currentRow, currentColumn] != 1)
                    {
                        return false;
                    }
                } else if (matrix[currentRow, currentColumn] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static decimal SumNegativeOnAntiDiagonal(decimal[,] matrix)
        {
            decimal sum = 0;

            var row = 0;
            for (var j = matrix.GetLength(0) - 1; j >= 0; j--)
            {
                var val = matrix[row++, j];
                if (val < 0)
                {
                    sum += val;
                }
            }

            return sum;
        }

        private static decimal[,] NormalizeRows(decimal[,] matrix)
        {
            var newMatrix = (decimal[,]) matrix.Clone();
            var row = newMatrix.GetLength(0);
            var col = newMatrix.GetLength(1);
            
            for (var i = 0; i < row; i++)
            {
                decimal sqrtSum = 0;
                for (var j = 0; j < col; j++)
                {
                    sqrtSum += newMatrix[i, j];
                }

                sqrtSum = new decimal(Math.Sqrt(decimal.ToDouble(sqrtSum)));
                for (var j = 0; j < col; j++)
                {
                    newMatrix[i, j] /= sqrtSum; // idk this is how I understood this part of the question
                }
            }

            return newMatrix;
        }

        private static decimal[,] SortMatrix(decimal[,] matrix)
        {
            var newMatrix = (decimal[,]) matrix.Clone();
            var row = newMatrix.GetLength(0);
            var col = newMatrix.GetLength(1);
            
            for (var i = 0; i < col; i++)
            {
                var column = GetMatrixColumn(newMatrix, i);
                var sortedColumn = i % 2 == 0 ? column.OrderBy(d => d).ToList() : column.OrderByDescending(d => d).ToList();

                for (var j = 0; j < row; j++)
                {
                    newMatrix[j, i] = sortedColumn[j];
                }
            }

            return newMatrix;
        }
        
        // lmao generics moment xDDDDD
        private static List<decimal> GetMatrixColumn(decimal[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToList();
        }
        
        private static int ParseInput(string s)
        {
            try
            {
                return int.Parse(s);
            }
            catch
            {
                Console.WriteLine("Bad input! Try again!");
                Environment.Exit(-1);
                return -1;
            }
        }
        
        public static void Main(string[] args)
        {
            var n = 1;
            var m = 1;
            var matrix = new decimal[n, m];

            using (StreamReader sr = File.OpenText("matrix.txt"))
            {
                string s;
                var row = 0;
                var inputLineCount = 1;

                while ((s = sr.ReadLine()) != null)
                {
                    if (inputLineCount <= 2) // can't have 1x1 matrix...
                    {
                        var input = ParseInput(s);
                        switch (inputLineCount++)
                        {
                            case 1:
                                n = input;
                                break;
                            case 2:
                                m = input;
                                matrix = new decimal[n, m];
                                break;
                        }
                        continue;
                    }

                    var elements = s.Trim().Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);

                    if (elements.Length < m)
                    {
                        Console.Write("Invalid matrix column length! Check your input, dumbass!");
                        Environment.Exit(-1);
                        return;
                    }

                    for (var elementIdx = 0; elementIdx < elements.Length; elementIdx++)
                    {
                        decimal val;
                        try
                        {
                            val = decimal.Parse(elements[elementIdx]);
                        }
                        catch
                        {
                            Console.Write("Bad parsing! Check your file, dumbass!");
                            Environment.Exit(-1);
                            return;
                        }
                        
                        matrix[row, elementIdx] = val;
                    }

                    row++;
                }

                if (row < n)
                {
                    Console.Write("Invalid matrix row length! Check your input, dumbass!");
                    Environment.Exit(-1);
                    return;
                }
                
                sr.Close();
            }

            PrintMatrix(matrix);
            Console.WriteLine();
            Console.WriteLine(CheckIdentity(matrix));
            Console.WriteLine(SumNegativeOnAntiDiagonal(matrix));
            PrintMatrix(NormalizeRows(matrix));
            Console.WriteLine();
            PrintMatrix(SortMatrix(matrix));
        }
    }
}