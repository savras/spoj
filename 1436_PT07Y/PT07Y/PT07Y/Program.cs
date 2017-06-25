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
                // DFS
                //SolveDfs(n, m);
                SolveDisjountSet(n, m);
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
        
        private static void SolveDisjountSet(int n, int m)
        {
            // Initialize disjoint set
            var ds = new int[n];
            for (var i = 0; i < n; i++)
            {
                ds[i] = i;
            }

            var hasCycle  = false;
            // Read edges and solve
            for (var i = 0; i < m && !hasCycle; i++)
            {
                var inputString = Console.ReadLine();
                var edge = inputString.Split(' ');
                var node1 = Convert.ToInt32(edge[0]) - 1;
                var node2 = Convert.ToInt32(edge[1]) - 1;
                
                var node1Parent = Find(ds, node1);
                var node2Parent = Find(ds, node2);

                if (node1Parent != node2Parent)
                {
                    ds[node1Parent] = node2Parent;
                }
                else
                {
                    hasCycle = true;
                }
            }

            // Check if all belongs to the same set (Not disjoint)
            var rootCount = 0;
            for (var i = 0; i < n && !hasCycle && rootCount <= 1; i++)
            {
                if (ds[i] == i)
                {
                    rootCount++;
                }
            }
            hasCycle = rootCount == 1;

            Console.WriteLine(hasCycle ? "NO" : "YES");
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

        private static void SolveDfs(int n, int m)
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

            var stack = new Stack<int>();
            var visited = new HashSet<int> {0};
            var hasCycle = Dfs(adj, visited, stack, 0, false);
            var hasVisitedAllNodes = visited.Count == n;

            Console.WriteLine(!hasCycle && hasVisitedAllNodes? "YES" : "NO");
        }
    }
}
