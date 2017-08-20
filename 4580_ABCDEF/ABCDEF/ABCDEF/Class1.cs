// Formula: ((a * b + c)/ d) - e = f
// Can be rewritten as: a * b + c = (f + e) * d
// Brute force, get permutation of all possible values in S for both  sides of the formula.
// Then, for each LFS (or each RHS), count the number of values that are equal on the other side.
// O(n^3) for permutation of 3 numbers out of n with repetition (n * n * n).

using System;

namespace ABCDEF
{
    public class Class1
    {
        int n = int.Parse(Console.ReadLine());
    }
}
