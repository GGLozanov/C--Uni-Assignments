using System;
using System.IO;

namespace UniAssignmentThree
{
    internal class Program
    {
        class FileStats
        { 
            private int commaCount;
            private int periodCount;
            private int wholeNumberCount;

            public FileStats(int wholeNumberCount, int commaCount, int periodCount)
            {
                this.commaCount = commaCount;
                this.periodCount = periodCount;
                this.wholeNumberCount = wholeNumberCount;
            }

            public override string ToString()
            {
                return "commaCount: " + commaCount + "; periodCount: " + periodCount + "; wholeNumberCount: " + wholeNumberCount;
            }
        }

        private static FileStats retrieveFileStats(string fileContents)
        {
            var commaCount = 0;
            var periodCount = 0;
            var wholeNumberCount = 0;

            var digitSeries = false;

            for (var i = 0; i < fileContents.Length; i++)
            {
                // if file is just 1 number
                if(digitSeries && i == fileContents.Length - 1)
                {
                    wholeNumberCount++;
                    break;
                }
                
                switch (fileContents[i])
                {
                    case '.':
                        periodCount++;
                        break;
                    case ',':
                        commaCount++;
                        break;
                }
                
                if (char.IsDigit(fileContents[i]))
                {
                    if(!digitSeries)
                    {
                        digitSeries = true;
                    }
                }
                else if(digitSeries)
                {
                    wholeNumberCount++;
                    digitSeries = false;
                }
            }

            return new FileStats(wholeNumberCount, commaCount, periodCount);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Name of file pls:");
            var filePath = Console.ReadLine();
            
            if(filePath == null)
            {
                Console.Write("No null, bucko.");
                return;
            }

            if (!File.Exists(filePath))
            {
                Console.Write("Don't exist, man.");
                return;
            }
            
            using (StreamReader sr = File.OpenText(filePath))
            {
                var s = sr.ReadToEnd();
                var stats = retrieveFileStats(s);
                
                Console.Write(stats);
            }
        }
    }
}