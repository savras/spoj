using System;
using System.Collections.Generic;

namespace BUGLIFE
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            var inputLine = Console.ReadLine();
            var inputSplit = inputLine.Split(' ');
            var b = int.Parse(inputSplit[0]);
            var interactions = int.Parse(inputSplit[1]);

            for (var i = 0; i < t; i++)
            {
                // var result = SolveBasic(b, interactions);
                var result = SolveBfs(b, interactions);
                Console.WriteLine($"Scenario {i + 1}:");
                Console.WriteLine(result ? "Suspicious  bugs found!" : "No suspicious bugs found!");
            }
        }

        private static bool SolveBfs(int bugCount, int interactions)
        {
            var adjList = new List<int>[bugCount];

            // Build Adj list
            for (var i = 0; i < interactions; i++)
            {
                var line = Console.ReadLine();
                var split = line.Split(' ');
                var a = int.Parse(split[0]);
                var b = int.Parse(split[1]);

                adjList[a].Add(b);
                adjList[b].Add(a);
            }

            var isSuspicious = false;
            // Bfs
            var visited = new HashSet<int>();
            var q = new Queue<int>(0);

            while (q.Count > 0)
            {
                var node = q.Dequeue();
                foreach (var neighbour in adjList[node])
                {
                    if (!visited.Contains(neighbour))
                    {
                        visited.Add(neighbour);
                        q.Enqueue(neighbour);
                    }
                }
            }

            return isSuspicious;
        }

        // DOES NOT WORK for the following input:
        // 1     Graph looks like this:
        // 4 4   (white) 3----4 (red)
        // 1 2           |    |
        // 3 4     (red) 1----2 (white)
        // 2 4 
        // 3 1 
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
    }
}