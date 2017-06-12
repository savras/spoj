﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PT07ZLongestPathInTree
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build Adj list
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
                //adjList[v].Add(u);
            }

            // Process
            var visited = new HashSet<int>();
            visited.Add(0);

            var neighbourLength = adjList[0]
                .Select(node => Dfs(adjList, visited, node, 1)).ToList();

            var sortedNeighbourLength = neighbourLength.OrderByDescending(nl => nl).ToList();
            var neighbourSize = adjList[0].Count;

            var maxLength = neighbourSize > 1 ? sortedNeighbourLength[0] + sortedNeighbourLength[1] : sortedNeighbourLength[0];
            Console.WriteLine(maxLength);
        }

        private static int Dfs(List<int>[] adjList, HashSet<int> visited, int current, int count)
        {
            var max = count;
            foreach (var node in adjList[current])
            {
                if (!visited.Contains(node))
                {
                    visited.Add(node);
                    max = Math.Max(max, Dfs(adjList, visited, node, ++count));
                    count--;
                }
            }

            return max;
        }
    }
}
