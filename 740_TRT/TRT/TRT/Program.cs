using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRT
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var input = Console.ReadLine();
            var inputSplit = input.Split(' ');
            var arr = new int[n];
            for (var i = 0; i < n; i++)
            {
                arr[i] = int.Parse(inputSplit[i]);
            }

            var result = FindPathRecursive(arr, 0, n - 1, 1);

            Console.WriteLine(result);
        }

        static int FindPathRecursive(int[] arr, int i, int j, int day)
        {
            if (i > j)
            {
                return 0;
            }

            return Math.Max(
                FindPathRecursive(arr, i + 1, j, day + 1) + (arr[i]*day),
                FindPathRecursive(arr, i, j - 1, day + 1) + (arr[j]*day)
                );
        }
    }
}
