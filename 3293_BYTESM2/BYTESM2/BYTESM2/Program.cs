using System;

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
                var rowNum = Convert.ToInt32(rowColSplit[0]);
                var colNum = Convert.ToInt32(rowColSplit[1]);

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

                var arrDp = new int[rowNum, colNum];
                for (int i = 0; i < colNum; i++)
                {
                    arrDp[0, i] = arr[0, i];
                }

                // Solve
                for (var row = 0; row < rowNum - 1; row++)
                {
                    for (var col = 0; col < colNum; col++)
                    {
                        if (col == 0)
                        {
                            arrDp[row + 1, col]     = Math.Max(arrDp[row + 1, col], arr[row + 1, col] + arrDp[row, col]);
                            arrDp[row + 1, col + 1] = Math.Max(arrDp[row + 1, col + 1], arr[row + 1, col + 1] + arrDp[row, col]);
                        }
                        else if (col > 0 && col < colNum - 1)
                        {
                            arrDp[row + 1, col]     = Math.Max(arrDp[row + 1, col], arr[row + 1, col] + arrDp[row, col]);
                            arrDp[row + 1, col + 1] = Math.Max(arrDp[row + 1, col + 1], arr[row + 1, col + 1] + arrDp[row, col]);
                            arrDp[row + 1, col - 1] = Math.Max(arrDp[row + 1, col - 1], arr[row + 1, col - 1] + arrDp[row, col]);
                        }
                        else if (col == colNum - 1)
                        {
                            arrDp[row + 1, col]     = Math.Max(arrDp[row + 1, col], arr[row + 1, col] + arrDp[row, col]);
                            arrDp[row + 1, col - 1] = Math.Max(arrDp[row + 1, col - 1], arr[row + 1, col - 1] + arrDp[row, col]);
                        }
                    }
                }

                // Get Max from bottom row
                var result = 0;
                var arrayOffset = 1;
                for (var i = 0; i < colNum; i++)
                {
                    result = Math.Max(result, arrDp[rowNum - arrayOffset, i]);
                }
                Console.WriteLine(result);
            }
        }
    }
}
