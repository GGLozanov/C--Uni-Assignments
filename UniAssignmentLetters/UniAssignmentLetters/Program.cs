using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace UniAssignmentLetters
{
    internal class Program
    {
        private static SortedDictionary<char, int> CalculateMinLettersForWordCombos(List<string> words)
        {
            var letterDict = new SortedDictionary<char, int>();

            foreach (var word in words)
            {
                var wordLetterDict = new SortedDictionary<char, int>();
                for (var i = 0; i < word.Length; i++)
                {
                    var letter = word[i];
                    if (!wordLetterDict.ContainsKey(letter))
                    {
                        wordLetterDict.Add(letter, 1);
                    }
                    else
                    {
                        wordLetterDict[letter] += 1;
                    }
                }

                wordLetterDict.ToList().ForEach(pair =>
                {
                    var key = pair.Key;
                    var value = pair.Value;

                    if (letterDict.ContainsKey(key))
                    {
                        if (letterDict[key] < value)
                        {
                            letterDict[key] = value;
                        }
                    }
                    else
                    {
                        letterDict.Add(key, value);
                    }
                });
            }

            return letterDict;
        }

        public static void Main(string[] args)
        {
            var words = new List<string>();
            
            using (StreamReader sr = File.OpenText("large_sample.txt"))
            {
                string s; 
                while ((s = sr.ReadLine()) != null)
                {
                    if (!Regex.IsMatch(s, "[a-zA-Z]+$"))
                    {
                        Console.WriteLine("Not a valid input string! Should contain only letters from the Latin-based alphabet!");
                        continue;
                    }
                    
                    words.Add(s);
                }
                
                sr.Close();
            }

            var result = CalculateMinLettersForWordCombos(words);
            var lastKey = result.Keys.Max();
            foreach (var letterCountPair in result)
            {
                for (var i = 0; i < letterCountPair.Value; i++)
                {
                    Console.Write(letterCountPair.Key);

                    if (!(lastKey == letterCountPair.Key && i == (letterCountPair.Value - 1)))
                    {
                        Console.Write(",");
                    }
                }
            }
        }
    }
}