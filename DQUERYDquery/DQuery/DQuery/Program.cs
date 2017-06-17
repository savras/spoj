using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace DQuery
{
    class Program
    {
        static int getNext(int currentIndex)
        {
            return currentIndex += (currentIndex & (-currentIndex));
        }

        static int getParent(int currentIndex)
        {
            return currentIndex & (--currentIndex);
        }

        static void update(List<int> bit, int index, int value)
        {
            do
            {
                bit[index] += value;
                index = getNext(index);
            } while (index <= bit.Count);
        }

        static int getUniqueElements(List<int> bit, int l, int r)
        {
            var sum = 0;
            for (var i = l; i <= r; i++)
            {
                sum += bit[i];
            }

            return sum;
        }

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var arr = new int[n];
            var nInput = Console.ReadLine();
            var nSplit = nInput.Split(' ');
            var strArr = nSplit.ToArray();

            for (var i = 0; i < n; i++)
            {
                arr[i] = int.Parse(strArr[i]);
            }

            var prefixSum = BuildPrefixSum(arr, n);
            var sortedPrefixSum = prefixSum.OrderByDescending(d => d).ToList();
            // Get D-queries
            var q = int.Parse(Console.ReadLine());
            for (var m = 0; m < q; m++)
            {
                var input = Console.ReadLine();
                var split = input.Split(' ');
                var i = int.Parse(split[0]);
                var j = int.Parse(split[1]);
                Console.WriteLine(sortedPrefixSum[--j] - sortedPrefixSum[--i] + 1);
            }
        }

        static List<int> BuildPrefixSum(int[] arr, int n)
        {
            var hs = new HashSet<int>();
            var prefixSum = new List<int> {1};
            hs.Add(arr[0]);

            for (int i = 1; i < n; i++)
            {
                prefixSum.Add(prefixSum[i - 1]);
                if (!hs.Contains(arr[i]))
                {
                    hs.Add(arr[i]);
                    prefixSum[i]++;
                }
            }
            return prefixSum;
        }
    }
}
