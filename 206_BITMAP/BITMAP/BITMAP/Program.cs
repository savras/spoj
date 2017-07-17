using System;
using System.CodeDom;
using System.Collections.Generic;

namespace BITMAP
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            var line = Console.ReadLine();
            var lineSplit = line.Split(' ');
            var n = int.Parse(lineSplit[0]);
            var m = int.Parse(lineSplit[1]);

            var arr = new int[n, m];
            var visitedArr = new int[n, m];
            var onePositions = new List<Tuple<int, int>>();

            for (var i = 0; i < n; i++)
            {
                var rowLine = Console.ReadLine();
                var rowSplit = rowLine.Split(' ');
                for (var j = 0; j < m; j++)
                {
                    var value = int.Parse(rowSplit[0]);
                    if (value == 1)
                    {
                        onePositions.Add(new Tuple<int, int>(i, j));
                        arr[i, j] = 0;
                    }
                    else
                    {
                        arr[i, j] = 9999;
                    }
                }
            }

            var hs = new Dictionary<int, int>();
            // Solve DP
            foreach (var cell in onePositions)
            {
                var queue = new Queue<Tuple<int, int>>();
                queue.Enqueue(new Tuple<int, int>(cell.Item1, cell.Item2));
                queue.Enqueue(new Tuple<int, int>(-1, -1));
                var cost = 0;
                while(queue.Count > 0)
                {
                    var item = queue.Dequeue();
                    if (item.Item1 == -1 && item.Item2 == -1)
                    {
                        queue.Enqueue(new Tuple<int, int>(-1, -1));
                        cost++;
                        continue;
                    }

                    var i = cell.Item1;
                    var j = cell.Item2;
                    if (arr[i, j] != 0 && cost < arr[i, j])
                    {
                        arr[i, j] = cost;
                    }

                    if (i > 0) { queue.Enqueue(new Tuple<int, int>(i - 1, j)); }
                    if (i < m) { queue.Enqueue(new Tuple<int, int>(i + 1, j)); }
                    if (j > 0) { queue.Enqueue(new Tuple<int, int>(i, j - 1)); }
                    if (j < n) { queue.Enqueue(new Tuple<int, int>(i, j + 1)); }
                }
            }
        }
    }
}
