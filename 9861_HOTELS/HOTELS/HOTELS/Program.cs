using System;

namespace HOTELS
{
    class Program
    {
        static int getMaxOfNextSet(int[] arr, int i, int max)
        {
            var index = i;

            return index;
        }

        static void Main(string[] args)
        {
            var line = Console.ReadLine();
            var lineSplit = line.Split(' ');
            var n = int.Parse(lineSplit[0]);
            var m = int.Parse(lineSplit[1]);

            var inputLine = Console.ReadLine();
            var inputSplit = inputLine.Split(' ');

            var arr = new int[n];
            for (var i = 0; i < n; i++)
            {
                arr[i] = int.Parse(inputSplit[i]);
            }

            // Solve
            ulong max = 0;

            int i = 0;
            for (i = 0; i < n; i++)
            {
                max += (ulong) arr[i];
                if (max > (ulong)m)
                {
                    max -= (ulong) arr[i];
                    break;
                }
            }

            // Sliding window
            for (var j = i; j < n; j++)
            {
                max += (ulong) arr[j] - (ulong) arr[j - i];

            }
        }
    }
}
