/*
 * Solution 1: O(n) - push all values into a hashset. If the values doesn't exist in the hashset, add to sum,
 *                    else if the value already exists, deduct from sum.
 *                    
 * Solution 2: O(n) - Use concept from XOR Swap:
 *             X := X XOR Y
 *             Y := Y XOR X
 *             X := X XOR Y
 */
using System;
using System.Collections.Generic;

namespace OLOLO
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            ProcessUsingXor(n);
            //ProcessUsingSum(n);
        }

        private static void ProcessUsingXor(int n)
        {
            var result = int.Parse(Console.ReadLine());
            for (var i = 1; i < n; i++)
            {
                result ^= int.Parse(Console.ReadLine());
            }
            Console.WriteLine(result);
        }

        private static void ProcessUsingSum(int n)
        {
            var hs = new HashSet<int>();
            var sum = 0;
            for (var i = 0; i < n; i++)
            {
                var val = int.Parse(Console.ReadLine());
                if (hs.Contains(val))
                {
                    sum -= val;
                }
                else
                {
                    sum += val;
                    hs.Add(val);
                }
            }

            Console.WriteLine(Math.Abs(sum));
        }
    }
}
