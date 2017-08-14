using System;

namespace MIXTURES
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            for (var i = 0; i < t; i++)
            {
                var n = int.Parse(Console.ReadLine());
                var line = Console.ReadLine();
                var split = line.Split(' ');
                var chemicals = Array.ConvertAll(split, int.Parse);

                var mixture = new int[n, n];
                var smoke = new int[n, n];

                for (var j = 0; j < n; j++)
                {
                    for (var k = j; k >= 0; k++)
                    {
                        mixture[j, k] = -1;
                    }
                }
                // Fill memoSum and memoMultiplication diagonally.
                for (var j = 1; j < n; j++)
                {
                    for (var k = j - 1; k >= 0; k--)
                    {
                        var smokeAmount = 0;
                        if (mixture[j - 1, k] == -1 || mixture[j, k + 1] == -1)
                        {
                            mixture[j, k] = (chemicals[j] + chemicals[k])%100;
                        }
                        else
                        {
                            var mixtureTop = (mixture[j - 1, k] + chemicals[j])%100;
                            var mixtureRight = (mixture[j, k + 1] + chemicals[k])%100;
                            if (mixtureTop + chemicals[j] <= mixtureRight + chemicals[k])
                            {
                                mixture[j, k] = mixtureTop + chemicals[j];
                                smokeAmount = mixture[j, k] * chemicals[j];
                            }
                            else
                            {
                                mixture[j, k] = mixtureRight + chemicals[k];
                                smokeAmount = mixture[j, k] * chemicals[k];
                            }
                        }

                        smoke[j, k] = smokeAmount;
                    }
                }

                Console.WriteLine(smoke[0, n-1]);
            }
        }
    }
}
