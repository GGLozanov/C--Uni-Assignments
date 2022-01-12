using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace UniAssignmentPreExam
{
    internal class Program
    {
        private static int MostCommonCount(decimal[] array)
        {
            var countMap = new SortedDictionary<decimal, int>();

            foreach (var num in array)
            {
                if (!countMap.ContainsKey(num))
                {
                    countMap.Add(num, 1);
                }
                else
                {
                    countMap[num] += 1;
                }
            }

            return countMap.OrderByDescending(d => d.Value).First().Value;
        }
        
        private static decimal[] ReverseInplace(decimal[] array)
        {
            var start = 0;
            var finish = array.Length - 1;

            while (start < finish)
            {
                (array[start], array[finish]) = (array[finish], array[start]);

                start++;
                finish--;
            }

            return array;
        }
        
        private static decimal[] ReadArrayFromFile(string fileName)
        {
            decimal[] array = null;

            using (StreamReader sr = File.OpenText(fileName))
            {
                string s;

                while ((s = sr.ReadLine()) != null)
                {
                    var stringInput = s.Trim().Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries);
                    array = stringInput.Select(c => decimal.Parse(c)).ToArray(); // add error handling if you want zzz
                }

                sr.Close();
            }

            return array;
        }
        
        public static void Main(string[] args)
        {
            var fileName = Console.ReadLine();

            var array = ReadArrayFromFile(fileName);

            var arrayReversed = ReverseInplace(array);
            foreach (var num in arrayReversed)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine();

            var mostCommonElementCount = MostCommonCount(array);
            Console.WriteLine("Most common element: ");
            Console.Write(mostCommonElementCount);
        }
    }
}