using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UniAssignmentFour
{
    internal class Program
    {
        private static Tuple<int, int> sumEvenAndOddElements(int[,] matrix, int n, bool evenRowsOddColumnsSum = false)
        {
            var sumEven = 0;
            var sumOdd = 0;

            for (var i = 0; i < matrix.Length; i++)
            {
                var element = matrix[i % n, i / n];
                if ((!evenRowsOddColumnsSum && element % 2 == 0) || (evenRowsOddColumnsSum && ((i % n) + 1) % 2 == 0))
                {
                    sumEven += element;
                }
                else
                {
                    sumOdd += element;
                }
            }

            return new Tuple<int, int>(sumEven, sumOdd);
        }

        private static void printMatrix(int[,] matrix, int n)
        {
            for (var i = 0; i < matrix.Length; i++)
            {
                if (i % n == 0)
                {
                    Console.WriteLine();
                }
                
                Console.Write(matrix[i % n, i / n]);
                Console.Write(" ");
            }
        } 
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter :");
            var nStr = Console.ReadLine();

            if (nStr == null)
            {
                Console.WriteLine("No null, bitch!");
                return;
            }

            int n;
            try
            {
                n = int.Parse(nStr);
            }
            catch
            {
                Console.WriteLine("Not an integer!");
                return;
            }

            int[,] matrix = new int[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    matrix[i, j] = -1;
                }
            }

            using (StreamReader sr = File.OpenText("C:\\Users\\GRIGS\\Desktop\\VPRAssignments\\UniAssignmentTwo\\UniAssignmentTwo\\assets\\matrix (1).txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var elements = s.Trim().Split('\t');
                    var parsedElements = elements.Select(str =>
                    {
                        try
                        {
                            return int.Parse(str);
                        }
                        catch
                        {
                            Console.Write("Bad parsing! Check your file, dumbass!");
                            Environment.Exit(-1);
                        }

                        return -1; // same paradigm as the other assignment
                    }).ToList();

                    if (parsedElements.Count < 3)
                    {
                        Console.WriteLine("Invalid parsed elements! Too few!");
                        continue;
                    }

                    if (parsedElements[0] >= n || parsedElements[1] >= n)
                    {
                        Console.WriteLine("File contains indices above N!");
                        continue;
                    }

                    matrix[parsedElements[0] - 1, parsedElements[1] - 1] = parsedElements[2];
                }

                printMatrix(matrix, n);
                Console.WriteLine();
                Console.WriteLine(sumEvenAndOddElements(matrix, n));
                Console.WriteLine(sumEvenAndOddElements(matrix, n, true));
            }
        }
    }
}