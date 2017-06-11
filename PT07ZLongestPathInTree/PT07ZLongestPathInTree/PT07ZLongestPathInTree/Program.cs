using System;
using System.Collections.Generic;

namespace PT07ZLongestPathInTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var adjList = new List<int>[n];

            for (int i = 0; i < n; i++)
            {
                adjList[i] = new List<int>();
            }

            for (var i = 0; i < n - 1; i++)
            {
                var line = Console.ReadLine();
                var inArr = line.Split(' ');

                var u = int.Parse(inArr[0]);
                var v = int.Parse(inArr[1]);
                u--; v--;
                adjList[u].Add(v);
            }

            var pathCount = 0;
            var max = 0;
            var visited = new HashSet<int>();
            Dfs(adjList, visited, 0, ref pathCount, ref max);

            Console.WriteLine(max);
        }

        static void Dfs(List<int>[] adjList, HashSet<int> visited, int current, ref int pathCount, ref int max)
        {
            var neighbours = new List<int>();
            neighbours.AddRange(adjList[current]);
            foreach (var node in neighbours)
            {
                if (!visited.Contains(node))
                {
                    visited.Add(node);
                    pathCount++;
                    Dfs(adjList, visited, node, ref pathCount, ref max);

                    if (pathCount > max)
                    {
                        max = pathCount;
                    }
                    pathCount--;
                }

            }
        }
    }
}
