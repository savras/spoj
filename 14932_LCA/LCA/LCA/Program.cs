/* First naive implementation is to use DFS and get the path taken to find both nodes,
 * then iterate through them to find the first common node.
 * I will then extend my solution to method in here: https://www.topcoder.com/community/data-science/data-science-tutorials/range-minimum-query-and-lowest-common-ancestor/#Lowest%20Common%20Ancestor%20(LCA)
 * Other solutions:
 *      Heavy Light Decomposition/ Range Minimum Query (Segment Tree)
 *      Sparse tree.
 *  Similar questions: LCASQ, TALCA (https://www.codechef.com/problems/TALCA)
 */
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
            var testCases = int.Parse(Console.ReadLine());
            for (var i = 0; i < testCases; i++)
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

                // Method 1 in TopCoder tutorial using square root of sections
                // Preprocess list and build P array.
                var p = new int[n];
                var t = new int[n];
                var height = GetTreeHeight(adjList, 0, 0);

                var sqrtHeight = (int) Math.Sqrt(height);
                BuildT(adjList, sqrtHeight, t);
                DfsBuildP(adjList, p, t, sqrtHeight, 0, 0, 0);

                var q = int.Parse(Console.ReadLine());
                for (var j = 0; j < q; j++)
                {
                    var queryLine = Console.ReadLine();
                    var querySplit = queryLine.Split(' ');
                    var v1 = int.Parse(querySplit[0]) - 1;
                    var v2 = int.Parse(querySplit[1]) - 1;

                    // DFS recursive
                    //var visited1 = new List<int>();
                    //Dfs(adjList, visited1, 0, v1);
                    //
                    //var visited2 = new List<int>();
                    //Dfs(adjList, visited2, 0, v2);
                    //
                    //var lca = GetLca(visited1, visited2);
                    //Console.WriteLine(lca + 1);
                }
            }
        }

        private static void BuildT(List<List<int>> adjList, int sqrtHeight, int[] ints)
        {
            throw new NotImplementedException();
        }

        private static int GetTreeHeight(List<List<int>> adjList, int currentNode, int height)
        {
            if (!adjList[currentNode].Any())
            {
                return height;
            }

            var maxHeight = height;
            foreach (var neighbour in adjList[currentNode])
            {
                maxHeight = Math.Max(maxHeight, GetTreeHeight(adjList, neighbour, height + 1));
            }

            return maxHeight;
        }

        // t contains the parent node for currentNode
        // p contains the last node of either parent or grand parent section.
        private static void DfsBuildP(List<List<int>> adjList, int[] p, int[] t, int sqrtHeight, int parentOfSection, int currentNode, int currentLevel)
        {
            // Node is in first section
            if (t[currentNode] < sqrtHeight)
            {
                p[currentNode] = 0;
            }
            // Node is at the beginning of some section
            // Set to ancestor node that is the last of the previous section
            else if (t[currentNode] % sqrtHeight == 0)
            {
            }
            // Set to the ancestor node that is the last of the previous two sections
            else
            {
                //p[currentNode] = p[t[currentNode]];
            }
            
            foreach (var neighbour in adjList[currentNode])
            {
                DfsBuildP(adjList, p, t, sqrtHeight, parentOfSection, neighbour, currentLevel + 1);
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
