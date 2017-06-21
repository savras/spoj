using System;
using System.Collections.Generic;

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
                adj[node].Add(edge);
            }

            // DFS
            Solve(adj);
        }

        private static void Dfs(List<int>[] adj, HashSet<int> visited, Stack<int> stack, int currentNode)
        {
            var neighboutSize = adj[currentNode].Count;
            for (int i = 0; i < neighboutSize; i++)
            {
                stack.Push(adj[currentNode][i]);
            }

            while (stack.Count != 0)
            {
                var neighbour = stack.Pop();
                if (!visited.Contains(neighbour))
                {
                    visited.Add(neighbour);
                    Dfs(adj, visited, stack, neighbour);
                }
            }
        }

        private static void Solve(List<int>[] adj)
        {
            var stack = new Stack<int>();
            var visited = new HashSet<int>();
            Dfs(adj, visited, stack, 0);
        }
    }
}
