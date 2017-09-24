/*
 * Idea: Recursion + back tracking => DP.
 * Not that top down backtrackking gives the same result for each student below the current,
 * thus, we recurse and memoize the number of combinations for each result below the current student.
 * Extend this to DP.
 * Can't figure out the Bitmask method so I referred to these: 
 * https://discuss.codechef.com/questions/49669/assign-spoj-redynamic-programming-with-bitmask
 * https://discuss.codechef.com/questions/49559/dynamic-programming-bit-masking
 * https://www.topcoder.com/community/data-science/data-science-tutorials/a-bit-of-fun-fun-with-bits/
 * 
 * Solution explanation:
 * Given the following:
 * 3
 * 1 1 1        // student 2
 * 0 1 0        // student 1
 * 1 0 1        // student 0
 * 
 * Using hand tracing, we get the expanded recursion:
 * compare with                    ----------111-----------     // first student gets to choose from all subjects
 * student 1 (1 1 1)              |           |            |
 *                               011         101          110   // second student only gets to choose 2 of 3 subjects
 * student 2 (0 1 0)              |           |            |
 *                               001          X           100  
 * student 3 (1 0 1)              |                        |
 *                               000                       X
 * 
 * KEY POINT 1: Notice that, for all valid combinations (combinations where each student gets a subject), 
 *              the number of 1's is always compare with the same student. I.e. student 3's preferences get compared 
 *              with 001 and 100 (one 1), student 2's preferences are compared with 011, 101, and 110.
 *              Because of this key point, the number of valid assignments of subjects to students remain the same for 
 *              each combination.
 *              
 * KEY POINT 2: Base case for 000 is one. This means that we have reached a valid assignment for all students.
 * 
 * So, we loop from 001 until before (1 << n) to get all combinations. E.g. n = 4, we loop from 0001, 0010, 0011, 0100, ... 1111.
 * For each combination, we check the number of 1's to map to a student. For each student, we remove a 1 from the combination where
 * the student's preferences also has a one in the same position of the number. E.g. combination 0100 and student preference 0101 share a common 1 
 * in the 3rd position (starting from 0 from the right). We remove that one (change it to 0), then repeat the process for the next student.
 */

using System;

namespace ASSIGN
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            for (var i = 0; i < t; i++)
            {
                // Read input into 2d array
                var n = int.Parse(Console.ReadLine());
                var a = new int[n,n];
                for (var j = 0; j < n; j++)
                {
                    var line = Console.ReadLine();
                    var split = line.Split(' ');
                    for (var k = 0; k < n; k++)
                    {
                        a[j, k] = int.Parse(split[k]);
                    }
                }
                
                var dp = new ulong[(1 << n)];
                dp[0] = 1;

                //  loop through each combination. I.e. 0001 to 1111
                for (var m = 1; m < (1 << n); m++)  // As a result of looping from 1 to (1 << n), we are actually building dp bottom up (memoization) of smaller subproblems
                {
                    var noOfOnes = GetOneCount(m);
                    var studentIndex = noOfOnes - 1;

                    for (var k = 0; k < n; k++) // For each subject preference for a[studentIndex][k]
                    {
                        var currentSubjectPreference = a[studentIndex, k];

                        // If k'th subject is NOT this student's preference, continue.
                        if (currentSubjectPreference == 0) { continue; }

                        // If the preference is NOT available to be chosen, continue
                        if (((m >> (n - k - 1)) & 1) == 0) { continue; }


                        // If k is this student's favourite, then the result can be gotten from the result stored 
                        // for the next student down's dp array.
                        dp[m] += dp[~(1 << n - k - 1) & m];  // get dp for index with k's position set to 0 in m.
                    }
                }

                Console.WriteLine(dp[(1 << n ) - 1]);   // if n = 4, then dp[1111];
            }
        }

        private static int GetOneCount(int m)
        {
            var count = 0;
            var i = 0;
            while((m >> i) > 0)
            {
                count += 1 & (m >> i);
                i++;
            }
            return count;
        }

        #region Solution using Map & backtracking:
        //    static void Main(string[] args)
        //    {
        //        var t = int.Parse(Console.ReadLine());
        //        for (var i = 0; i < t; i++)
        //        {
        //            // Track the students and the subjects that they are interested in
        //            var dict = new Dictionary<ulong, HashSet<ulong>>();
        //            var n = int.Parse(Console.ReadLine());

        //            for (var j = 0; j < n; j++)
        //            {
        //                var line = Console.ReadLine();
        //                var split = line.Split(' ');

        //                var likedSubjects = new HashSet<ulong>(); ;
        //                for (var k = 0; k < n; k++)
        //                {
        //                    var value = ulong.Parse(split[k]);
        //                    if (value == 1)
        //                    {
        //                        likedSubjects.Add((ulong)k);
        //                    }
        //                }
        //                dict.Add((ulong)j, likedSubjects);
        //            }

        //            var result = SolveRecursive(dict, 0, new List<ulong>());
        //            Console.WriteLine(result);
        //        }
        //    }

        //    private static ulong SolveRecursive(Dictionary<ulong, HashSet<ulong>> dict, ulong current, List<ulong> chosen)
        //    {
        //        ulong sum = 0;

        //        if (chosen.Count == dict.Count)
        //        {
        //            return 1;
        //        }

        //        if (current >= (ulong)dict.Count)
        //        {
        //            return 0;
        //        }

        //        var remainingChoices = dict[current].Except(chosen).ToList();
        //        if (!remainingChoices.Any())
        //        {
        //            return 0;
        //        }

        //        foreach (var choice in remainingChoices)
        //        {
        //            chosen.Add(choice);
        //            sum += SolveRecursive(dict, current + 1, chosen);
        //            chosen.RemoveAt(chosen.Count - 1);
        //        }

        //        return sum;
        //    }
        //}
        #endregion
    }
}