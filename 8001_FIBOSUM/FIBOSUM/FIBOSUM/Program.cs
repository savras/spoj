using System;
using System.Collections.Generic;

namespace FIBOSUM
{
    class Program
    {
        private static readonly int[,] F = { { 1, 1 }, { 1, 0 } };

        static void MatrixExponentiationOptimized(int n, int m)
        {
            ulong sum = 0;
            var result = new[,] { { 1, 1 }, { 1, 0 } };
            
            for (var i = n; i <= m; i++)
            {
                if (i == 0)
                {
                    sum += 0;
                }
                else
                {
                    GetFibonacci(result, i);
                    sum += (ulong)result[0, 1]; // or result[1, 0];
                }

                result[0, 0] = 1;
                result[0, 1] = 1;
                result[1, 0] = 1;
                result[1, 1] = 0;
            }
            Console.WriteLine(sum % 1000000007);
        }

        // O(log n)
        static void GetFibonacci(int[,] result, int n)
        {
            if (n == 0 || n == 1)
            {
                return;
            }

            GetFibonacci(result, n/2);
            Multiply(result, result);   // This is what enables O(log n) solution.

            if (n%2 != 0)
            {
                Multiply(result, F);
            }
        }

        static void Multiply(int[,] result, int[,] arr)
        {
            var a = (arr[0, 0]*result[0, 0]) + (arr[0, 1]*result[1, 0]);
            var b = (arr[0, 0]*result[0, 1]) + (arr[0, 1]*result[1, 1]);
            var c = (arr[1, 0]*result[0, 0]) + (arr[1, 1]*result[1, 0]);
            var d = (arr[1, 0]*result[0, 1]) + (arr[1, 1]*result[1, 1]);

            result[0, 0] = a;
            result[0, 1] = b;
            result[1, 0] = c;
            result[1, 1] = d;
        }

        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());

            for (var i = 0; i < t; i++)
            {
                var line = Console.ReadLine();
                var split = line.Split(' ');
                var n = int.Parse(split[0]);
                var m = int.Parse(split[1]);

                MatrixExponentiationOptimized(n, m);
                // Dijkstra Fibonacci 
            }
        }
    }
}
