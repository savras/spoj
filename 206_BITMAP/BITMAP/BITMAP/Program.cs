using System;
using System.CodeDom;
using System.Collections.Generic;

namespace BITMAP
{
    class Program
    {
        static void InitializeVisited(bool[,] visited, int n, int m)
        {
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    visited[i, j] = false;
                }
            }
        }

        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            for (var test = 0; test < t; test++)
            {
                var line = Console.ReadLine();
                var lineSplit = line.Split(' ');
                var n = int.Parse(lineSplit[0]);
                var m = int.Parse(lineSplit[1]);

                var arr = new int[n, m];
                var visitedArr = new bool[n, m];
                var onePositions = new List<Tuple<int, int>>();

                for (var i = 0; i < n; i++)
                {
                    var rowLine = Console.ReadLine();
                    for (var j = 0; j < m; j++)
                    {
                        var value = int.Parse(rowLine[j].ToString());
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

                //SolveBfs(onePositions, arr, visitedArr, n, m);
                //SolveRecursive(onePositions, arr, n, m);
                SolveDp(onePositions, arr, n, m);

                PrintResult(n, m, arr);
                Console.ReadLine();
            }
        }

        private static void SolveDp(List<Tuple<int, int>> onePositions, int[,] arr, int i, int i1)
        {
            throw new NotImplementedException();
        }

        private static void SolveRecursive(List<Tuple<int, int>> onePositions, int[,] arr, int n, int m)
        {
            foreach (var pos in onePositions)
            {
                Recurse(pos.Item1, pos.Item2, arr, n, m, 0);
            }
        }

        private static void Recurse(int i, int j, int[,] arr, int n, int m, int cost)
        {
            arr[i, j] = cost;

            if (i > 0 && arr[i - 1, j] != 0 && arr[i - 1, j] > cost)
            {
                Recurse(i - 1, j, arr, n, m, cost + 1);
            }
            if (i < n - 1 && arr[i + 1, j] != 0 && arr[i + 1, j] > cost)
            {
                Recurse(i + 1, j, arr, n, m, cost + 1);
            }
            if (j > 0 && arr[i, j - 1] != 0 && arr[i, j - 1] > cost)
            {
                Recurse(i, j - 1, arr, n, m, cost + 1);
            }
            if (j < m - 1 && arr[i, j + 1] != 0 && arr[i, j + 1] > cost)
            {
                Recurse(i, j + 1, arr, n, m, cost + 1);
            }
        }

        private static void PrintResult(int n, int m, int[,] arr)
        {
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private static void SolveBfs(List<Tuple<int, int>> onePositions, int[,] arr, bool[,] visitedArr, int n, int m)
        {
            foreach (var cell in onePositions)
            {
                InitializeVisited(visitedArr, n, m);
                var queue = new Queue<Tuple<int, int>>();
                queue.Enqueue(new Tuple<int, int>(cell.Item1, cell.Item2));
                queue.Enqueue(new Tuple<int, int>(-1, -1));
                visitedArr[cell.Item1, cell.Item2] = true;

                var cost = 0;

                while (queue.Count > 0)
                {
                    var item = queue.Dequeue();
                    if (item.Item1 == -1 && item.Item2 == -1)
                    {
                        queue.Enqueue(new Tuple<int, int>(-1, -1));
                        if (queue.Peek().Item2 == -1 && queue.Peek().Item1 == -1)
                        {
                            break;
                        }

                        cost++;
                        continue;
                    }

                    var i = item.Item1;
                    var j = item.Item2;

                    arr[i, j] = cost;

                    if (i > 0 && !visitedArr[i - 1, j] && arr[i - 1, j] != 0 && arr[i - 1, j] > cost)
                    {
                        queue.Enqueue(new Tuple<int, int>(i - 1, j));
                        visitedArr[i - 1, j] = true;
                    }
                    if (i < n - 1 && !visitedArr[i + 1, j] && arr[i + 1, j] != 0 && arr[i + 1, j] > cost)
                    {
                        queue.Enqueue(new Tuple<int, int>(i + 1, j));
                        visitedArr[i + 1, j] = true;
                    }
                    if (j > 0 && !visitedArr[i, j - 1] && arr[i, j - 1] != 0 && arr[i, j - 1] > cost)
                    {
                        queue.Enqueue(new Tuple<int, int>(i, j - 1));
                        visitedArr[i, j - 1] = true;
                    }
                    if (j < m - 1 && !visitedArr[i, j + 1] && arr[i, j + 1] != 0 && arr[i, j + 1] > cost)
                    {
                        queue.Enqueue(new Tuple<int, int>(i, j + 1));
                        visitedArr[i, j + 1] = true;
                    }
                }
            }
        }
    }
}
