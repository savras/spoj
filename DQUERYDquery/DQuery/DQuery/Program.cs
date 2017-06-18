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
using System.Linq;

namespace DQuery
{
    class Program
    {
        /*
         * BIT Implementation
         */
        static int GetNext(int currentIndex)
        {
            return currentIndex + (currentIndex & (-currentIndex));
        }

        static int GetParent(int currentIndex)
        {
            return currentIndex & (currentIndex - 1);
        }

        private static int GetAt(List<int> bit, int index)
        {
            if (index == 0) { return 0; }
            int value = 0;
            do
            {
                value += bit[index];
                index = GetParent(index);
            } while (index > 0);

            return value;
        }

        static void UpdateAt(List<int> bit, int index, int value)
        {
            do
            {
                bit[index] += value;
                index = GetNext(index);
            } while (index < bit.Count);
        }

        static int GetUniqueElementsInRange(List<int> bit, int l, int r)
        {
            var sum = GetAt(bit, r) - GetAt(bit, l - 1);
            return sum;
        }

        static void SolveBIT(int[] arr, List<int> bit, List<Tuple<int, int>> sortedTuples, List<Tuple<int, int>> tuples, int[] resultOrder)
        {
            var dict = new Dictionary<int, int>();
            for (var i = 0; i < arr.Length; i++)
            {
                if (dict.ContainsKey(arr[i]))
                {
                    UpdateAt(bit, dict[arr[i]], -1);
                    dict[arr[i]] = i + 1;
                }
                else
                {
                    dict.Add(arr[i], i + 1);
                }
                UpdateAt(bit, dict[arr[i]], 1);

                foreach (var query in QueriesAtI(sortedTuples, i + 1))
                {
                    var result = GetUniqueElementsInRange(bit, query.Item1, query.Item2);
                    var index = tuples.IndexOf(query);
                    resultOrder[index] = result;
                }
            }
        }
        /*
         * End BIT
         */

        /*
         * MO's implementation
         */
        static void SolveMO()
        {
                                                         
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

            var resultOrder = new int[q];
            SolveBIT(arr, bit, sortedTuples, tuples, resultOrder);

            for (var i = 0; i < resultOrder.Length; i++)
            {
                if (i == resultOrder.Length - 1)
                {
                    Console.Write(resultOrder[i]);
                }
                else
                {
                    Console.WriteLine(resultOrder[i]);
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
