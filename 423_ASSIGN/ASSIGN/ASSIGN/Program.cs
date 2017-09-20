/*
 * Idea: Recursion + back tracking => DP.
 * Not that top down backtrackking gives the same result for each student below the current,
 * thus, we recurse and memoize the number of combinations for each result below the current student.
 * Extend this to DP.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace ASSIGN
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            for (var i = 0; i < t; i++)
            {
                // Track the students and the subjects that they are interested in
                var dict = new Dictionary<int, HashSet<int>>();
                var n = int.Parse(Console.ReadLine());

                for (var j = 0; j < n; j++)
                {
                    var line = Console.ReadLine();
                    var split = line.Split(' ');

                    var likedSubjects = new HashSet<int>(); ;
                    for (var k = 0; k < n; k++)
                    {
                        var value = int.Parse(split[k]);
                        if (value == 1)
                        {
                            likedSubjects.Add(k);
                        }
                    }
                    dict.Add(j, likedSubjects);
                }

                var result = SolveRecursive(dict, 0, new List<int>());
                Console.WriteLine(result);
            }
        }

        private static ulong SolveRecursive(Dictionary<int, HashSet<int>> dict, int current, List<int> chosen)
        {
            ulong sum = 0;

            if (chosen.Count == dict.Count)
            {
                return 1;
            }

            if (current >= dict.Count)
            {
                return 0;
            }

            var remainingChoices = dict[current].Except(chosen).ToList();
            if (!remainingChoices.Any())
            {
                return 0;
            }

            foreach (var choice in remainingChoices)
            {
                chosen.Add(choice);
                sum += SolveRecursive(dict, current + 1, chosen);
                chosen.RemoveAt(chosen.Count - 1);
            }

            return sum;
        }
    }
}