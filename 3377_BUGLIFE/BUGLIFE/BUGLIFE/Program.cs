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

            for(var i = 0; i < t; i++)
            {
                var result = SolveBasic(b, interactions);
                Console.WriteLine($"Scenario {i + 1}:");
                Console.WriteLine(result ? "Suspicious  bugs found!" : "No suspicious bugs found!");
            }
        }

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
