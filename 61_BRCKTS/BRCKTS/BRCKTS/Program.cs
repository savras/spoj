/* Solution: '(' represents +1.
 *           ')' represents -1. 
 *           Use prefix array concept and check that the value at every index is positive, and 
 *           the value in the last index is 0.
 *           Best implementation of this concept is using a segment tree where for every update we only need to
 *           check the values in thee node that we update up to the root have the following properties:
 *           Non-root nodes have values >= 0, and root node has value == 0. This yields O(n log n) for initial
 *           build of tree + log n updates/checks.
 */

using System;
using System.Collections.Generic;

namespace BRCKTS
{
    class Program
    {
        private static bool isValid = true;

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var brackets = Console.ReadLine();
            var m = int.Parse(Console.ReadLine());

            // Number of leaf nodes in a full tree is 2^h, where h is height of the tree.
            // Therefore, 2^h = n. Via log identity, h = log n.
            var height = (int)Math.Ceiling(Math.Log(n, 2));
            var leafNodes = (int) Math.Pow(2, height);
            var size =  (leafNodes * 2) - 1;    // Number of leaves in level before leaf nodes in full tree is just the no. of leaf notes - 1.
            var tree = new int[size];

            BuildIntervalTree(brackets, tree, 0, n - 1, 0);

            for (var i = 0; i < m; i++)
            {
                var operation = int.Parse(Console.ReadLine());
                if (operation == 0)
                {
                    CheckTree();
                }
                else
                {
                    UpdateTree(operation - 1, tree, 0, n - 1, 0);
                    if (tree[0] != 0)
                    {
                        isValid = false;
                    }
                }
            }
        }

        static void CheckTree()
        {
            // ToDo:: Cater for initial check as well.
            Console.WriteLine(isValid ? "YES" : "NO");
        }

        static int UpdateTree(int index, int[] tree, int bracketLow, int bracketHigh, int current)
        {
            if (bracketLow >= bracketHigh)
            {
                tree[current] *= -1;
                return tree[current] * 2;
            }

            int updatedValue;
            var mid = bracketLow + (bracketHigh - bracketLow)/2;
            if(index >= bracketLow && index <= mid)
            {
                updatedValue = UpdateTree(index, tree, bracketLow, mid, (2 * current) + 1);
            }
            else
            {
                updatedValue = UpdateTree(index, tree, mid + 1, bracketHigh, (2 * current) + 2);
            }

            tree[current] += updatedValue;  // Need to also remove the old value of the inversed bracket.
            if (tree[current] < 0 && isValid)
            {
                isValid = false;
            }
            return updatedValue;
        }

        static int  BuildIntervalTree(string brackets, int[] tree, int low, int high, int current)
        {
            if (low >= high)
            {
                var value = brackets[low] == ('(') ? 1 : -1;
                tree[current] = value;
                return value;
            }

            var mid = low + (high - low)/2;
            var leftValue = BuildIntervalTree(brackets, tree, low, mid, (2 * current) + 1);
            var rightValue = BuildIntervalTree(brackets, tree, mid + 1, high, (2 * current) + 2);
            tree[current] = leftValue + rightValue;
            return tree[current];
        }
    }
}
