using System;

namespace HOTELS
{
    class Program
    {
        static void Main(string[] args)
        {
            var line = Console.ReadLine();
            var lineSplit = line.Split(' ');
            var n = int.Parse(lineSplit[0]);
            var m = int.Parse(lineSplit[1]);

            var inputLine = Console.ReadLine();
            var inputSplit = inputLine.Split(' ');

            var arr = new int[n];
            for (var p = 0; p < n; p++)
            {
                arr[p] = int.Parse(inputSplit[p]);
            }

            // Solve
            ulong max = 0;
            ulong sum = 0;

            var i = 0;
            var j = 0;

            while (j < n)
            {
                if (arr[j] > m)
                {
                    j++;
                    i = j;
                    sum = 0;
                }

                while (j < n && sum <= (ulong) m)
                {
                    sum += (ulong) arr[j];
                    j++;
                }
                max = Math.Max(max, max -  (ulong) arr[j - 1]);

                while (sum > (ulong) m)
                {
                    sum -= (ulong) arr[i];
                    i++;
                }

                j++;
            }
        }
    }
}