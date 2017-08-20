// Formula: ((a * b + c)/ d) - e = f                // O(n^5)
// Can be rewritten as: (a * b) + c = d * (f + e)     // O(n^3)
// Brute force, get permutation of all possible values in S for both  sides of the formula.
// Then, for each LFS (or each RHS), count the number of values that are equal on the other side.
// O(n^3) for permutation of 3 numbers out of n with repetition (n * n * n).

// Update: Looks like the brute force method is the way to go but its implementation can be optimized.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Source
{
    class Program
    {
        static void Main(string[] args)
        {
            var lhs = new Dictionary<int, int>();
            var rhs = new Dictionary<int, int>();

            var n = int.Parse(Console.ReadLine());
            var s = new int[n];

            for (var i = 0; i < n; i++)
            {
                var element = int.Parse(Console.ReadLine());
                s[i] = element;
            }

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    for (var k = 0; k < n; k++)
                    {
                        var lhsKey = (s[i]*s[j]) + s[k];
                        var rhsKey = s[i]*(s[j] + s[k]);

                        if (!lhs.ContainsKey(lhsKey))
                        {
                            lhs.Add(lhsKey, 0);
                        }
                        lhs[lhsKey]++;

                        if (s[i] != 0)
                        {
                            if (!rhs.ContainsKey(rhsKey))
                            {
                                rhs.Add(rhsKey, 0);
                            }
                            rhs[rhsKey]++;
                        }
                    }
                }
            }

            var count = 0;

            foreach (var item in lhs)
            {
                if (rhs.ContainsKey(item.Key))
                {
                    count += (item.Value*rhs[item.Key]);
                }
            }

            Console.WriteLine(count);
        }
    }
}
