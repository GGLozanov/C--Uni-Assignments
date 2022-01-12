using System;
using System.Collections.Generic;

namespace UniAssignmentPreExam2
{
    internal class Program
    {
        private static int CountWords(string text)
        {
            int wordCount = 0;
            
            for (var i = 0; i < text.Length; i++)
            {
                if (!char.IsLetter(text[i]) && (i != 0 && char.IsLetter(text[i - 1]))) // or check with ASCII codes
                {
                    wordCount++;
                }
            }

            return wordCount;
        }

        private static int CountDigits(string text)
        {
            int digitCount = 0;
            for (var i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i])) // or, again, check with ASCII codes
                {
                    digitCount++;
                }
            }

            return digitCount;
        }

        private static SortedDictionary<char, int> LettersHistogram(string text)
        {
            var letterMap = new SortedDictionary<char, int>();

            for (var i = 0; i < text.Length; i++)
            {
                if ((text[i] >= 'a' && text[i] <= 'z') || (text[i] >= 'A' && text[i] <= 'Z'))
                {
                    var upper = char.ToUpper(text[i]);
                    if (!letterMap.ContainsKey(upper))
                    {
                        letterMap.Add(upper, 1);
                    }
                    else
                    {
                        letterMap[upper] += 1;
                    }
                }
            }

            return letterMap;
        }

        public static void Main(string[] args)
        {
            var text = String.Empty;

            string line;
            while ((line = Console.ReadLine()) != string.Empty)
            {
                text += line;
            }
            
            Console.WriteLine(CountWords(text));
            Console.WriteLine(CountDigits(text));

            var histogram = LettersHistogram(text);
            
            foreach (var pair in histogram)
            {
                Console.WriteLine("'{0}' -> {1}", pair.Key, pair.Value);
            }
        }
    }
}