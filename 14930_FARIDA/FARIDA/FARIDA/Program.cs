/*
 * For dpArr[i], its value is the sum the current monster coin value, i, and the sum of either of the following:
 * (Note that we can't select dpArr[i - 1] because of the specification)
 * 1) The sum of dpArr[i - 2], which means that we have selected dpArr[i - 2].
 * 2) The sum of dpArr[i - 3], which means that on the previous selection, we 
 *    have deliberately skipped selecting from the next two cells (Skipped the next even 
 *    cell if current cell is even, or next odd cell if current cell is odd)
 *    |current| forced to skip | skip this | select this |
 */
using System;

namespace FARIDA
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());

            for (var i = 0; i < t; i++)
            {
                var n = Convert.ToInt32(Console.ReadLine());
                var coins = new int[n];

                var line = Console.ReadLine();
                var lineSplit = line.Split(' ');
                for (var j = 0; j < n; j++)
                {
                    var coin = Convert.ToInt32(lineSplit[j]);
                    coins[j] = coin;
                }

                if (n == 1)
                {
                    Console.WriteLine("Case {0}: {1}", i + 1, coins[0]);
                    continue;
                }

                // Solve
                var dpArr = new ulong[n];
                for (var j = 0; j < n; j++)
                {
                    var iMinusTwo = j > 1 ? dpArr[j - 2] : 0;
                    var iMinusThree = j > 2 ? dpArr[j - 3] : 0;

                    var max = Math.Max(iMinusTwo, iMinusThree);
                    dpArr[j] = (ulong)coins[j] + max;
                }

                var arrayOffset = 1;

                Console.WriteLine("Case {0}: {1}", i + 1, Math.Max(dpArr[n - arrayOffset], dpArr[n - arrayOffset - 1]));
            }
        }
    }
}
