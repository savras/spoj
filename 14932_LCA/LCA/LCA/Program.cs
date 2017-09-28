using System;
using System.Collections.Generic;
using System.Linq;

namespace LCA
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build adjacency list
            var t = int.Parse(Console.ReadLine());
            for (var i = 0; i < t; i++)
            {
                var adjList = new List<List<int>>();
                var n = int.Parse(Console.ReadLine());
                for (var j = 0; j < n; j++)
                {
                    var line = Console.ReadLine();
                    var split = line.Split(' ');

                    adjList.Add(new List<int>());

                    var length = split.Length;
                    for (var k = 1; k < length; k++)
                    {
                        adjList[j].Add(int.Parse(split[k]) - 1); // Offset to 0 since root is 0 in our implementation
                    }
                }


                var q = int.Parse(Console.ReadLine());
                for (var j = 0; j < q; j++)
                {
                    var queryLine = Console.ReadLine();
                    var querySplit = queryLine.Split(' ');
                    var v1 = int.Parse(querySplit[0]) - 1;
                    var v2 = int.Parse(querySplit[1]) - 1;

                    // DFS recursive
                    var visited1 = new List<int>();
                    Dfs(adjList, visited1, 0, v1);

                    var visited2 = new List<int>();
                    Dfs(adjList, visited2, 0, v2);

                    var lca = GetLca(visited1, visited2);
                    Console.WriteLine(lca + 1);
                }
            }
        }

        private static int GetLca(IReadOnlyList<int> path1, IReadOnlyList<int> path2)
        {
            var l1Len = path1.Count - 1;
            var l2Len = path2.Count - 1;

            var indexToCompare = Math.Min(l1Len, l2Len);

            while (path1[indexToCompare] != path2[indexToCompare])
            {
                indexToCompare--;
            }

            return path1[indexToCompare];
        }

        private static bool Dfs(IReadOnlyList<List<int>> adjList, List<int> visited, int current, int value)
        {
            if (value == current)
            {
                visited.Add(current);
                return true;
            }

            var found = false;
            if (!visited.Contains(value) && !visited.Contains(current))
            {
                visited.Add(current);
                foreach (var neighbour in adjList[current])
                {
                    found = Dfs(adjList, visited, neighbour, value);

                    if (found)
                    {
                        break;
                    }
                }

                if (!found)
                {
                    visited.RemoveAt(visited.Count - 1);
                }
            }
            return found;
        }
    }
}
