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
                var inputArr = Array.ConvertAll(split, int.Parse);

                var memoSum = new int[n, n];    // Mixture
                var memoMultiplication = new int[n, n]; // Smoke

                // Fill memoSum and memoMultiplication diagonally.
                for (var j = 1; j < n; j++)
                {
                    for (var k = j - 1; k >= 0; k--)
                    {
                        var multiplication = 0;
                        if (memoSum[j - 1, k] == -1 || memoSum[j, k + 1] == -1)
                        {
                            memoSum[j, k] = (inputArr[j] + inputArr[k])%100;
                        }
                        else
                        {
                            var sumTop = (memoSum[j - 1, k] + inputArr[j])%100;
                            var sumRight = (memoSum[j, k + 1] + inputArr[k])%100;
                            if (sumTop > sumRight)
                            {
                                memoSum[j, k] = sumTop;
                                multiplication = inputArr[j] + memoSum[j, k];
                            }
                            else
                            {
                                memoSum[j, k] = sumRight;
                                multiplication = inputArr[k] + memoSum[j, k];
                            }
                        }

                        memoMultiplication[j, k] = Math.Max(memoSum[j - 1, k], memoSum[j, k + 1]) + multiplication;
                    }
                }
            }
        }
    }
}
