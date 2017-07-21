using System;
using System.Collections.Generic;

namespace BUGLIFE
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());

            for (var i = 0; i < t; i++)
            {
                var inputLine = Console.ReadLine();
                var inputSplit = inputLine.Split(' ');
                var bugCount = int.Parse(inputSplit[0]);
                var interactions = int.Parse(inputSplit[1]);

                // Prepare adj List
                var adjList = new List<int>[bugCount];
                for (var j = 0; j < bugCount; j++)
                {
                    adjList[j] = new List<int>();
                }

                // Build Adj list
                for (var j = 0; j < interactions; j++)
                {
                    var line = Console.ReadLine();
                    var split = line.Split(' ');
                    var a = int.Parse(split[0]) - 1;
                    var b = int.Parse(split[1]) - 1;

                    adjList[a].Add(b);
                    adjList[b].Add(a);
                }

                // var result = SolveBasic(b, interactions);
                // var result = SolveBfs(b, interactions);
                var result = SolveBfsOptimized(bugCount, adjList, interactions);

                Console.WriteLine($"Scenario #{i + 1}:");
                Console.WriteLine(result ? "Suspicious bugs found!" : "No suspicious bugs found!");
            }
        }

        private static bool SolveBfsOptimized(int bugCount, IReadOnlyList<List<int>> adjList, int interactions)
        {
            var colour = new bool?[bugCount];
            var q = new Queue<int>();

            int c = 0;
            var next = 0;
            var isSuspicious = false;

            while (next != -1 && !isSuspicious)
            {
                q.Enqueue(next);
                colour[next] = true;
                while (q.Count > 0 && !isSuspicious)
                {
                    var node = q.Dequeue();
                    foreach (var neighbour in adjList[node])
                    {
                        if (colour[neighbour] == null)
                        {
                            q.Enqueue(neighbour);
                            colour[neighbour] = !colour[node];
                        }
                        if (colour[node] == colour[neighbour])
                        {
                            isSuspicious = true;
                        }
                    }
                }
                next = GetNext(colour, bugCount, ref c);
            }

            return isSuspicious;
        }

        private static int GetNext(bool?[] colour, int bugCount, ref int c)
        {
            for (var i = c; i < bugCount; i++)
            {
                if (colour[i] == null)
                {
                    c = i;
                    return i;
                }
            }
            return -1;
        }

        #region Old Code
        // 1 
        // 6 4 
        // 1 2 
        // 2 3 
        // 4 5 
        // 5 6 
        // No suspicious bugs found!
        private static bool SolveBfs(int bugCount, int interactions)
        {
            var adjList = new List<int>[bugCount];
            for(var i = 0; i < bugCount; i++)
            {
                adjList[i] = new List<int>();
            }

            // Build Adj list
            for (var i = 0; i < interactions; i++)
            {
                var line = Console.ReadLine();
                var split = line.Split(' ');
                var a = int.Parse(split[0]) - 1;
                var b = int.Parse(split[1]) - 1;

                adjList[a].Add(b);
                adjList[b].Add(a);
            }

            // Bfs
            var colours = new[] { new HashSet<int>(), new HashSet<int>() };
            var isSuspicious = false;

            var q = new Queue<int>();
            var visited = new HashSet<int>();

            var nextNode = GetNextVisited(visited, bugCount); ;
            while (nextNode != -1 && !isSuspicious)
            {
                visited.Add(nextNode);
                q.Enqueue(nextNode);
                q.Enqueue(-1);
                colours[0].Clear();
                colours[1].Clear();
                var colourIndex = 1;
                var flip = -1;
                colours[0].Add(nextNode);

                while (q.Count > 0 && !isSuspicious)
                {
                    var node = q.Dequeue();
                    if (node == -1)
                    {
                        q.Enqueue(-1);
                        colourIndex += flip;
                        flip *= -1;
                        if (q.Peek() == -1)
                        {
                            q.Clear();
                            break;  // Finished processing.
                        }
                        continue;
                    }

                    foreach (var neighbour in adjList[node])
                    {
                        if (!visited.Contains(neighbour))
                        {
                            visited.Add(neighbour);
                            q.Enqueue(neighbour);
                        }
                        colours[colourIndex].Add(neighbour);
                        if (colours[colourIndex + flip].Contains(neighbour))
                        {
                            isSuspicious = true;
                            break;
                        }
                    }
                }
                nextNode = GetNextVisited(visited, bugCount);
            }
            
            return isSuspicious;
        }

        static int GetNextVisited(HashSet<int> visited, int bugCount)
        {
            for (var i = 0; i < bugCount; i++)
            {
                if (!visited.Contains(i))
                {
                    return i;
                }
            }
            return -1;
        }

        // DOES NOT WORK for the following input:
        // 1     Graph looks like this:
        // 4 4   (white) 3----4 (red)
        // 1 2           |    |
        // 3 4     (red) 1----2 (white)
        // 2 4 
        // 3 1 
        // No suspicious bugs found!
        private static bool SolveBasic(int s, int interactions)
        {
            var isSuspicious = false;
            var hs1 = new HashSet<int>();
            var hs2 = new HashSet<int>();

            for (var i = 0; i < interactions; i++)
            {
                var line = Console.ReadLine();
                var split = line.Split(' ');
                var a = int.Parse(split[0]);
                var b = int.Parse(split[1]);

                if (!hs1.Contains(a) && !hs2.Contains(a) && !hs1.Contains(b) && !hs2.Contains(b))
                {
                    // a && b don't exist in both sets
                    hs1.Add(a);
                    hs2.Add(b);
                }
                else if ((hs1.Contains(a) && hs1.Contains(b)) ||
                  (hs2.Contains(a) && hs2.Contains(b)))
                {
                    isSuspicious = true;
                    break;
                }
                else
                {
                    // a exists in hs1
                    if (hs1.Contains(a) || hs2.Contains(a))
                    {
                        if (hs1.Contains(a))
                        {
                            hs2.Add(b);
                        }
                        else
                        {
                            hs1.Add(b);
                        }
                    }
                    else
                    {
                        if (hs1.Contains(b))
                        {
                            hs2.Add(a);
                        }
                        else
                        {
                            hs1.Add(a);
                        }
                    }
                }
            }

            return isSuspicious;
        }
        #endregion
    }
}