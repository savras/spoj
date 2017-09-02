/* Solution: '(' represents +1.
 *           ')' represents -1. 
 *           Use prefix array concept and check that the value at every index is positive, and 
 *           the value in the last index is 0.
 */

using System;

namespace BRCKTS
{
    class Program
    {
        private static bool isValid = false;

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
                    UpdateTree(operation - 1, tree);
                }
            }
        }

        static void CheckTree()
        {
            Console.WriteLine(isValid ? "YES" : "NO");
        }

        static void UpdateTree(int index, int[] tree)
        {
            
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
