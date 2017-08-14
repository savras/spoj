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
                    for (var k = 0; k < n; k++)
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
                            smokeAmount = chemicals[j] * chemicals[k];
                        }
                        else
                        {
                            var mixtureTop = mixture[j - 1, k];
                            var mixtureRight = mixture[j, k + 1];

                            var topPlusCurrent = (mixtureTop + chemicals[j])%100;
                            var rightPlusCurrent = (mixtureRight + chemicals[k])%100;
                            if (topPlusCurrent <= rightPlusCurrent)
                            {
                                mixture[j, k] = topPlusCurrent;
                                smokeAmount = (mixtureTop * chemicals[j]) + smoke[j - 1, k];
                            }
                            else
                            {
                                mixture[j, k] = rightPlusCurrent;
                                smokeAmount = (mixtureRight * chemicals[k]) + smoke[j, k - 1]; 
                            }
                        }

                        smoke[j, k] = smokeAmount;
                    }
                }

                Console.WriteLine(smoke[n-1, 0]);
            }
        }
    }
}
