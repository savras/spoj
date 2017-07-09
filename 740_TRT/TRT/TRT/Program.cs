using System;
using System.Collections.Generic;

namespace TRT
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var arr = new int[n];
            for (var i = 0; i < n; i++)
            {
                var value = int.Parse(Console.ReadLine());
                arr[i] = (value);
            }

            
            var memo = new ulong[n, n];
            var result = FindPathRecursive(arr, 0, n - 1, 1, memo);
            Console.WriteLine(result);

            var memoDp = new ulong[n, n];
            var resultDp = FindPathDp(arr, memoDp, n);
            Console.WriteLine(resultDp);
        }

        private static ulong FindPathDp(int[] arr, ulong[,] memo, int n)
        {
            FillBottomLeftToTopRightDiagonal(arr, memo, n);

            var offset = 1;
            for (var i = n - 1 - offset; i >= 0; i--)   // Top right most row and column cell is already filled, so we start with the cell below that.
            {
                for (var j = i + 1; j < n; j++)
                {
                    var dayValue = i + 1;   // Because our array index starts with is 0 when day should be 1
                    // Value of treat for the next day
                    ulong topCell = memo[i + 1, j];
                    ulong leftCell = memo[i, j - 1];

                    ulong value;
                    if (topCell > leftCell)
                    {
                        var currentDayTreatValue = arr[i]*dayValue;
                        value = topCell + (ulong)currentDayTreatValue;
                    }
                    else
                    {
                        var currentDayTreatValue = arr[j]*dayValue;
                        value = leftCell + (ulong)currentDayTreatValue;
                    }
                    memo[i, j] = value;
                }
            }

            return memo[0, n - offset];
        }

        private static void FillBottomLeftToTopRightDiagonal(IReadOnlyList<int> arr, ulong[,] memo, int n)
        {
            // Base case diagonal is filled with the treats multiplied by the last day
            var lastDayValue = n;   // Just for clarity, number of input == number of days since we can only pick one treat per day.
            for (var i = 0; i < n; i++)
            {
                memo[i, i] = (ulong) (arr[i]*(lastDayValue));
            }
        }

        static ulong FindPathRecursive(int[] arr, int i, int j, int day, ulong[,] memo)
        {
            if (i > j)
            {
                return 0;
            }


            if (memo[i, j] != 0)
            {
                return memo[i, j];
            }

            var max = Math.Max(
                FindPathRecursive(arr, i + 1, j, day + 1, memo) + (ulong)(arr[i]*day),
                FindPathRecursive(arr, i, j - 1, day + 1, memo) + (ulong)(arr[j]*day)
                );

            memo[i, j] = max;
            return max;
        }
    }
}