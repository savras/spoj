using System;
using System.Configuration;

namespace BYTESM2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());
            for (var p = 0; p < t; p++)
            {
                // Initialize
                var rowCol = Console.ReadLine();
                var rowColSplit = rowCol.Split(' ');
                var rowNum = rowCol[0];
                var colNum = rowCol[1];

                var arr = new int[rowNum, colNum];

                for (var i = 0; i < rowNum; i++)
                {
                    var line = Console.ReadLine();
                    var lineSplit = line.Split(' ');
                    for (var j = 0; j < colNum; j++)
                    {
                        arr[i, j] = Convert.ToInt32(lineSplit[j]);
                    }
                }

                // Solve
                for (var row = 0; row < rowNum - 1; row++)
                {
                    for (var col = 0; col < colNum; col++)
                    {
                        int bottomRight;
                        int bottomLeft;
                        var bottom = arr[row - 1, col];

                        int value;
                        if (col == 0)
                        {
                            bottomRight = arr[row - 1, col + 1];
                            value = Math.Max(bottom, bottomRight);
                        }
                        else if (col > 0 && col < rowNum - 1)
                        {
                            bottomRight = arr[row - 1, col + 1];
                            bottomLeft = arr[row - 1, col - 1];
                            value = Math.Max(bottom, Math.Max(bottomRight, bottomLeft));
                        }
                        else
                        {
                            bottomLeft = arr[row - 1, col - 1];
                            value = Math.Max(bottom, bottomLeft);
                        }

                        arr[row, col] = value;
                    }
                }

                // Get Max from bottom row
                var result = 0;
                var arrayOffset = 1;
                for (var i = 0; i < colNum; i++)
                {
                    result = Math.Max(result, arr[rowNum - arrayOffset, i]);
                }
                Console.WriteLine(result);
            }
        }
    }
}
