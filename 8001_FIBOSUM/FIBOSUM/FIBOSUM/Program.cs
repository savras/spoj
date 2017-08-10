using System;

namespace FIBOSUM
{
    class Program
    {

        static void MatrixExponentiation(int n, int m)
        {
            var F = new[,] {{1, 1}, {1, 0}};
            var result = new[,] { { 1, 1 }, { 1, 0 } };

            for (var i = 2; i < n; i++)
            {
                var a = (F[0, 0] * result[0, 0]) + (F[0, 1] * result[1, 0]);
                var b = (F[0, 0] * result[0, 1]) + (F[0, 1] * result[1, 1]);
                var c = (F[1, 0] * result[0, 0]) + (F[1, 1] * result[1, 0]);
                var d = (F[1, 0] * result[0, 1]) + (F[1, 1] * result[1, 1]);

                result[0, 0] = a;
                result[0, 1] = b;
                result[1, 0] = c;
                result[1, 1] = d;
            }

            var fibN = result[0, 0];
        }

        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            var line = Console.ReadLine();
            var split = line.Split(' ');
            var n = int.Parse(split[0]);
            var m = int.Parse(split[1]);

            for (var i = 0; i < t; i++)
            {
                MatrixExponentiation(n, m);
                // Dijkstra Fibonacci 
            }
        }
    }
}
