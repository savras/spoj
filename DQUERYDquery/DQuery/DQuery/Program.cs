// Helpful links:
// https://stackoverflow.com/questions/18553567/difficulty-in-understanding-the-approach-for-solving-spoj-dquery
// https://stackoverflow.com/questions/27656135/spoj-dquery-tle-even-with-bit
// Key: The BIT will be storing the index of latest occurence of each number in the original array at each iteration.
// E.g. arr = {1, 1, 2, 1, 3}: BIT will contain all zeroes except for BIT[3] = 1, BIT[4] = 1, BIT[5] = 1.
// The last occurence of digit 1 in the arr is at index 4 (since BIT uses index 0 as a placeholder).
// Offline/Online solution: Offline means that we can read in all the queries and preprocess them. Online means that we have to read in the queries
// one line at a time.

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            } while (index < bit.Count);
        }

        static int getUniqueElementsInRange(List<int> bit, int l, int r)
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
            
            // Get offline D-queries
            var tuples = new List<Tuple<int, int>>();
            var q = int.Parse(Console.ReadLine());
            for (var m = 0; m < q; m++)
            {
                var input = Console.ReadLine();
                var split = input.Split(' ');
                var i = int.Parse(split[0]);
                var j = int.Parse(split[1]);
                
                tuples.Add(new Tuple<int, int>(i, j));
            }
            var sortedTuples = tuples.OrderBy(t => t.Item2).ToList();

            // Solve
            var bit = new List<int>(n + 1);
            for (var i = 0; i < n + 1; i++)
            {
                bit.Add(0);
            }

            Solve(arr, bit, sortedTuples);

        }

        static void Solve(int[] arr, List<int> bit, List<Tuple<int, int>> sortedTuples)
        {
            var dict = new Dictionary<int, int>();
            // Loop through array
            // each iteration update previous occureing index of arr[i] to current.
                // or update bit[i] only,m8
            // each iteration check if i == r
            // if so, get answer.

            for (var i = 0; i < arr.Length; i++)
            {
                if (dict.ContainsKey(arr[i]))
                {
                    update(bit, dict[arr[i]], -1);
                    dict[arr[i]] = i + 1;
                }
                else
                {
                    dict.Add(arr[i], i + 1);
                }
                update(bit, dict[arr[i]], 1);

                foreach(var query in QueriesAtI(sortedTuples, i + 1))
                {
                    Console.WriteLine(getUniqueElementsInRange(bit, query.Item1, query.Item2));
                }
            }
        }

        public static IEnumerable<Tuple<int, int>> QueriesAtI(List<Tuple<int, int>>  tuples, int i)
        {
            foreach (var tuple in tuples)
            {
                if (tuple.Item2 == i)
                {
                    yield return tuple;
                }
            }
        }
    }
}
