using System;

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
            else {
                var max = Math.Max(
                FindPathRecursive(arr, i + 1, j, day + 1, memo) + (ulong)(arr[i]*day),
                FindPathRecursive(arr, i, j - 1, day + 1, memo) + (ulong)(arr[j]*day)
                );

                memo[i, j] = max;
                return max;
            }
        }
    }
}