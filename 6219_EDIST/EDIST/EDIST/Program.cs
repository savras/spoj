using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Solve(arr);
            }
        }

        private static void Solve(int[,] arr)
        {
            throw new NotImplementedException();
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
