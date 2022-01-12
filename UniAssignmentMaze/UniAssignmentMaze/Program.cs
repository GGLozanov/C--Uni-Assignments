using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UniAssignmentMaze
{
    internal class Program
    {
        private class MazePoint {
            readonly int x;
            readonly int y;
            readonly MazePoint parent;

            public MazePoint(int x, int y, MazePoint parent) {
                this.x = x;
                this.y = y;
                this.parent = parent;
            }

            // Encapsulation's important, kiddos!
            public int GetX()
            {
                return x;
            }

            public int GetY()
            {
                return y;
            }

            public MazePoint GetParent()
            {
                return parent;
            }

            public override string ToString()
            {
                return "(" + x + ", " + y + ")";
            }
        }
        
        private static void PrintMatrix(int[,] matrix)
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
            Console.WriteLine();
        }
        
        // BFS lol. For lab1.txt this outputs the alternate path that's possible
        private static Queue<KeyValuePair<int, int>> ShortestPathInMaze(int[,] matrix, int yStart, int xStart,
            int yExit, int xExit)
        {
            var mazeMatrix = (int[,]) matrix.Clone();
            
            var queue = new Queue<MazePoint>();
            queue.Enqueue(new MazePoint(xStart, yStart, null));

            while (queue.Count != 0)
            {
                var point = queue.Dequeue();

                var pointY = point.GetY();
                var pointX = point.GetX();

                if (pointY == yExit && pointX == xExit)
                {
                    var indicesQueue = new Queue<KeyValuePair<int, int>>();
                    
                    while (point != null)
                    {
                        indicesQueue.Enqueue(new KeyValuePair<int, int>(point.GetX(), point.GetY()));
                        point = point.GetParent();
                    }
                    
                    return new Queue<KeyValuePair<int, int>>(indicesQueue.Reverse());
                }

                MazePoint newPoint;
                
                // left
                if(IsMazeCellFree(pointX + 1, pointY, mazeMatrix, xExit, yExit)) {
                    mazeMatrix[pointY, pointX] = -1; // mark as 'visited'/'checked' (the BFS way!)
                    newPoint = new MazePoint(pointX + 1, pointY, point);
                    queue.Enqueue(newPoint);
                }

                // right
                if(IsMazeCellFree(pointX - 1, pointY, mazeMatrix, xExit, yExit)) {
                    mazeMatrix[pointY, pointX] = -1;
                    newPoint = new MazePoint(pointX - 1, pointY, point);
                    queue.Enqueue(newPoint);
                }

                // bottom
                if(IsMazeCellFree(pointX, pointY + 1, mazeMatrix, xExit, yExit))
                {
                    mazeMatrix[pointY, pointX] = -1;
                    newPoint = new MazePoint(pointX, pointY + 1, point);
                    queue.Enqueue(newPoint);
                }

                // top
                if(IsMazeCellFree(pointX, pointY - 1, mazeMatrix, xExit, yExit)) {
                    mazeMatrix[pointY, pointX] = -1;
                    newPoint = new MazePoint(pointX, pointY - 1, point);
                    queue.Enqueue(newPoint);
                }
            }
            
            return null;
        }
        
        private static bool IsMazeCellFree(int x, int y, int[,] matrix, int xEnd, int yEnd) {
            var rowLength = matrix.GetLength(0);
            var colLength = matrix.GetLength(1);
            
            return (x >= 0 && x < rowLength) && (y >= 0 && y < colLength) && (matrix[y, x] == 0 || (y == yEnd && x == xEnd));
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
        
        // imagine seeing this method in production code lol
        private static void VerifyInputValidIndex(int input, int n)
        {
            if (input < n) return;
            Console.WriteLine("Input over N!");
            Environment.Exit(-1);
        }

        public static void Main(string[] args)
        {
            int n = 1;
            var xStart = 1;
            var yStart = 1;
            var xExit = 4;
            var yExit = 4;
            var matrix = new int[1,1];
            
            using (StreamReader sr = File.OpenText("lab2.txt"))
            {
                string s;
                var inputLineCount = 1;
                var row = 0;
                
                while ((s = sr.ReadLine()) != null)
                {
                    if (inputLineCount <= 5)
                    {
                        var parsedInput = ParseInput(s);
                        switch(inputLineCount++)
                        {
                            case 1:
                                n = parsedInput;
                                matrix = new int[n, n];
                                break;
                            case 2:
                                VerifyInputValidIndex(parsedInput, n);
                                xStart = parsedInput;
                                break;
                            case 3:
                                VerifyInputValidIndex(parsedInput, n);
                                yStart = parsedInput;
                                break;
                            case 4:
                                VerifyInputValidIndex(parsedInput, n);
                                xExit = parsedInput;
                                break;
                            case 5:
                                VerifyInputValidIndex(parsedInput, n);
                                yExit = parsedInput;
                                break;
                        }
                        continue;
                    }

                    var elements = s.Trim().Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries); // hey, you - thanks for leaving those unnecessary whitespaces on row 2 & 3. You suck :)
                    for (var elementIdx = 0; elementIdx < elements.Length; elementIdx++)
                    {
                        var val = ParseInput(elements[elementIdx]);
                        
                        if (val != 1 && val != 0)
                        {
                            Console.WriteLine("Bad input! Not a boolean matrix!");
                            Environment.Exit(-1);
                            return;
                        }

                        matrix[row, elementIdx] = val;
                    }

                    row++;
                }
                
                sr.Close();
            }
            
            var shortestPath = ShortestPathInMaze(matrix, yStart, xStart, yExit, xExit);
            
            if (shortestPath == null)
            {
                Console.WriteLine("NO PATH");
                return;
            }
            
            foreach (var keyValuePair in shortestPath)
            {
                Console.Write("({0}, {1}) -> ", keyValuePair.Key, keyValuePair.Value);
            }
            Console.Write("EXIT");
        }
    }
}