using System;

namespace EDIST
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 0;
            n = int.Parse(Console.ReadLine());
            for (var i = 0; i < n; i++)
            {
                // Prepare array
                var s1 = Console.ReadLine();
                var s2 = Console.ReadLine();
                var s1Size = s1.Length;
                var s2Size = s2.Length;

                var arr = new int[s1Size + 1, s2Size + 1];
                InitializeArray(arr);

                // Solve
                Solve(arr, s1, s2);
            }
        }

        private static void Solve(int[,] arr, string s1, string s2)
        {
            for (var i = 1; i < arr.GetLength(0); i++)
            {
                for (var j = 1; j < arr.GetLength(1); j++)
                {
                    var left = arr[i - 1, j];
                    var top = arr[i, j - 1];
                    var diagonal = arr[i - 1, j - 1];
                    var min = Math.Min(left, Math.Min(diagonal, top));
                    arr[i, j] = min + 1;

                    if (s1[i - 1] == s2[j - 1])
                    {
                        arr[i, j]--;
                    }
                }
            }

            Console.WriteLine(arr[s1.Length, s2.Length]);
        }

        private static void InitializeArray(int[,] arr)
        {
            for (var i = 0; i < arr.GetLength(0); i++)
            {
                arr[i, 0] = i;
            }

            for (var i = 0; i < arr.GetLength(1); i++)
            {
                arr[0, i] = i;
            }
        }
    }
}
