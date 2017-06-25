/*
 * 1. Graph should be connected. (All nodes should be visited)
 * 2. No. of cycles in graph
 * 3. No. of edges == No. of nodes - 1. 
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace PT07Y
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var split = input.Split(' ');
            var n = int.Parse(split[0]);
            var m = int.Parse(split[1]);

            if (m != n - 1)
            {
                Console.WriteLine("NO");
            }
            else
            {
                // Build adj list
                var adj = new List<int>[n];
                for (int i = 0; i < n; i++)
                {
                    adj[i] = new List<int>();
                }

                for (int i = 0; i < m; i++)
                {
                    var inEdge = Console.ReadLine();
                    var inSplit = inEdge.Split(' ');
                    var node = int.Parse(inSplit[0]);
                    var edge = int.Parse(inSplit[1]);
                    adj[node - 1].Add(edge - 1);
                }

                // DFS
                //SolveDfs(adj, n);
                SolveDisjountSet(adj, n);
            }
        }

        // Get root
        private static int Find(int[] ds, int x)
        {
            if (ds[x] == x)
            {
                return x;
            }
            return Find(ds, ds[x]);
        }

        private static void Union(int[] ds, int x, int y)
        {
            var xParent = Find(ds, x);
            var yParent = Find(ds, y);

            if (xParent != yParent)
            {
                ds[x] = yParent;
            }
        }
        
        private static void SolveDisjountSet(List<int>[] adj, int n)
        {
            // Initialize disjoint set
            var ds = new int[n];
            for (var i = 0; i < n; i++)
            {
                ds[i] = i;
            }

            foreach (var node in adj)
            {
                var currentParent = Find(ds, node[0]);
                foreach (var neighbour in node)
                {
                    var neighbourParent = Find(ds, neighbour);
                    Union(ds, currentParent, neighbourParent);
                }
            }
        }

        private static bool Dfs(List<int>[] adj, HashSet<int> visited, Stack<int> stack, int currentNode, bool hasCycle)
        {
            var neighbourSize = adj[currentNode].Count;
            for (int i = 0; i < neighbourSize; i++)
            {
                stack.Push(adj[currentNode][i]);
            }

            while (stack.Count != 0 && !hasCycle)
            {
                var neighbour = stack.Pop();
                if (!visited.Contains(neighbour))
                {
                    visited.Add(neighbour);
                    hasCycle = Dfs(adj, visited, stack, neighbour, hasCycle);
                }
                else if(neighbour != currentNode)
                {
                    hasCycle = true;
                }
            }

            return hasCycle;
        }

        private static void SolveDfs(List<int>[] adj, int n)
        {
            var stack = new Stack<int>();
            var visited = new HashSet<int> {0};
            var hasCycle = Dfs(adj, visited, stack, 0, false);
            var hasVisitedAllNodes = visited.Count == n;

            Console.WriteLine(!hasCycle && hasVisitedAllNodes? "YES" : "NO");
        }
    }
}
