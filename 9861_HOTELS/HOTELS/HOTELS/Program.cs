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
            var m = ulong.Parse(lineSplit[1]);

            var inputLine = Console.ReadLine();
            var inputSplit = inputLine.Split(' ');

            var arr = new int[n];
            for (var p = 0; p < n; p++)
            {
                arr[p] = int.Parse(inputSplit[p]);
            }

            // Solve
            ulong max = 0;  // Good base candidate as question says required number will be greater than 0 in all test data.
            ulong sum = 0;

            var i = 0;
            var j = 0;

            while (j < n)
            {
                do
                {
                    sum += (ulong) arr[j];
                    j++;
                } while (j < n && sum <= m);
                

                j--;
                sum -= (ulong) arr[j];
                max = Math.Max(max, sum);

                sum += (ulong) arr[j];
                j++;

                while (sum > m && i < j)
                {
                    sum -= (ulong) arr[i];
                    i++;
                }

                max = Math.Max(max, sum);
            }

            Console.WriteLine(max);
        }
    }
}