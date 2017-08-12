using System;

namespace FIBOSUM
{
    class Program
    {

        static void MatrixExponentiationOptimized(int n, int m)
        {
            ulong sum = 0;
            for (var i = n; i <= m; i++)
            {
                var result = new[,] {{1, 1}, {1, 0}};
                if (i == 0)
                {
                    sum += 0;
                }
                else
                {
                    GetFibonacci(result, i);
                    sum += (ulong)result[0, 1]; // or result[1, 0];
                }
            }
            Console.WriteLine(sum);
        }

        // O(log n)
        static void GetFibonacci(int[,] result, int n)
        {
            if (n == 0 || n == 1)
            {
                return;
            }

            GetFibonacci(result, n/2);
            var F = new[,] {{1, 1}, {1, 0}};
            Multiply(result, result);

            if (n%2 != 0)
            {
                Multiply(result, F);
            }
        }

        static void Multiply(int[,] result, int[,] F)
        {
            var a = (F[0, 0]*result[0, 0]) + (F[0, 1]*result[1, 0]);
            var b = (F[0, 0]*result[0, 1]) + (F[0, 1]*result[1, 1]);
            var c = (F[1, 0]*result[0, 0]) + (F[1, 1]*result[1, 0]);
            var d = (F[1, 0]*result[0, 1]) + (F[1, 1]*result[1, 1]);

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
