using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UniAssignmentTwo
{
    internal class Program
    {
        private static Tuple<int, int> sumOfEvenOnUnevenRowsAndUnevenOnEvenColumns(List<List<int>> matrix)
        {
            var evenOnUnevenRowSum = 0;
            var unevenOnEvenColumnSum = 0;
            matrix.ForEach(row =>
            { 
               row.ForEach(element =>
               {
                   if (element % 2 == 0)
                   {
                       if ((matrix.IndexOf(row) + 1) % 2 != 0)
                       {
                           evenOnUnevenRowSum += element;
                       }
                   } else if ((row.IndexOf(element) + 1) % 2 == 0)
                   {
                       unevenOnEvenColumnSum += element;
                   }
               }); 
            });

            return new Tuple<int, int>(evenOnUnevenRowSum, unevenOnEvenColumnSum);
        }

        public static void Main(string[] args)
        {
            var matrix = new List<List<int>>();
            
            using (StreamReader sr = File.OpenText("matrix.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var elements = s.Trim().Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries); // hey, you - thanks for leaving those unnecessary whitespaces on row 2 & 3. You suck :)
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

                        return -1; // will never reach here but C# doesn't have Never returns like Kotlin or Swift so...
                    }).ToList();
                    
                    matrix.Add(parsedElements);
                }
            }

            var result = sumOfEvenOnUnevenRowsAndUnevenOnEvenColumns(matrix);
            
            Console.WriteLine("Even elements on uneven rows: " +
                          result.Item1);
            Console.Write("Uneven elements on even columns: " + result.Item2);
        }
    }
}