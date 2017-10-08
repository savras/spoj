/* First naive implementation is to use DFS and get the path taken to find both nodes,
 * then iterate through them to find the first common node.
 * I will then extend my solution to method in here: https://www.topcoder.com/community/data-science/data-science-tutorials/range-minimum-query-and-lowest-common-ancestor/#Lowest%20Common%20Ancestor%20(LCA)
 * Other solutions:
 *      Heavy Light Decomposition/ Range Minimum Query (Segment Tree)
 *      Sparse tree.
 *  Similar questions: LCASQ, TALCA (https://www.codechef.com/problems/TALCA)
 *  Complexity: <preprocessing, query> => <O(n), O(sqrt(H))>
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
                // O(n)
                var section = new int[n];
                var parent = new int[n];
                var level = new int[n];
                var height = GetTreeHeightAndBuildParent(adjList, parent, level, 0, 0);

                var sqrtHeight = (int) Math.Sqrt(height);
                
                DfsBuildSectionArray(adjList, section, parent, level, sqrtHeight, 0, 0, 0);

                // Query O(sqrt(n)) because number of times section[] traverses is equal number of sections => sqrt(h)
                var q = int.Parse(Console.ReadLine());
                for (var j = 0; j < q; j++)
                {
                    var queryLine = Console.ReadLine();
                    var querySplit = queryLine.Split(' ');
                    var v1 = int.Parse(querySplit[0]) - 1;
                    var v2 = int.Parse(querySplit[1]) - 1;

                    /* Find matching values in p for both v1 and v2, slowly going upwards for the section[node] with the larger value */
                    var result = FindCommonParent(parent, section, v1, v2);

                    Console.WriteLine(result + 1);

                    /* DFS recursive */
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

        private static int FindCommonParent(int[] parent, int[] section, int v1, int v2)
        {
            var sectionV1 = section[v1];
            var sectionV2 = section[v2];

            while (sectionV2 != sectionV1)
            {
                if (sectionV1 > sectionV2)
                {
                    sectionV1 = section[parent[sectionV1]];
                }
                else
                {
                    sectionV2 = section[parent[sectionV2]];
                }
            }

            return sectionV1;   // Both sectionV1 and sectionv2 will be the same
        }

        private static int GetTreeHeightAndBuildParent(List<List<int>> adjList, int[] parent, int[] level, int currentNode, int height)
        {
            level[currentNode] = height;
            if (!adjList[currentNode].Any())
            {
                return height;
            }

            var maxHeight = height;
            foreach (var neighbour in adjList[currentNode])
            {
                parent[neighbour] = currentNode;
                maxHeight = Math.Max(maxHeight, GetTreeHeightAndBuildParent(adjList, parent, level, neighbour, height + 1));
            }

            return maxHeight;
        }

        // parent contains the parent node for currentNode
        // section contains the last node of the previous section
        // level contains the level the current node is in the tree
        private static void DfsBuildSectionArray(List<List<int>> adjList, int[] section, int[] parent, int[] level, int sqrtHeight, int parentOfSection, int currentNode, int currentLevel)
        {
            // Node is in first section
            if (level[currentNode] < sqrtHeight)
            {
                section[currentNode] = 0;
            }
            // Node is at the beginning of some non-first section
            // Set to ancestor node that is the last of the previous section (basically its parent)
            else if (level[currentNode] % sqrtHeight == 0)
            {
                section[currentNode] = parent[currentNode];
            }
            // Set to the ancestor node that is the last of the previous two sections
            else
            {
                section[currentNode] = section[parent[currentNode]]; // We are building top down so this works. The tree only goes downwards.
            }
            
            foreach (var neighbour in adjList[currentNode])
            {
                DfsBuildSectionArray(adjList, section, parent, level, sqrtHeight, parentOfSection, neighbour, currentLevel + 1);
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
