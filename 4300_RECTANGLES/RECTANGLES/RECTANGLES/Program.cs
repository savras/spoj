// Solution idea:
// Reword of the question is: How many rectangles can you form given n squares of the same size?
// The number of rectangles that we can produce is the number of factors that each number from 1 to n has.
using System;

namespace RECTANGLES
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var result = 0;
            for (var i = 1; i <= n; i++)
            {
                var sqrt = (int)Math.Sqrt(i);   // Any more than sqrt of n we will be repeating the opposite of the multiplication. E.g. 2 * 1 => 1 * 2
                for (var j = 1; j <= sqrt; j++)
                {
                    if (i%j == 0)
                    {
                        result++;
                    }
                }
            }

            Console.WriteLine(result);  
        }
    }
}
