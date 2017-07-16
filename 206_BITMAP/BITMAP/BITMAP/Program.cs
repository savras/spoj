using System;
using System.CodeDom;
using System.Collections.Generic;

namespace BITMAP
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            var line = Console.ReadLine();
            var lineSplit = line.Split(' ');
            var n = int.Parse(lineSplit[0]);
            var m = int.Parse(lineSplit[1]);

            var bitOneHs = new HashSet<int>();
            var arr = new int[n, m];
            for (var i = 0; i < n; i++)
            {
                var rowLine = Console.ReadLine();
                var rowSplit = rowLine.Split(' ');
                for (var j = 0; j < m; j++)
                {
                    var value = int.Parse(rowSplit[0]);
                    if (value == 1)
                    {
                        bitOneHs.Add(value);
                    }
                    arr[i, j] = value;
                }
            }

            var hs = new Dictionary<int, int>();
            // Solve DP
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    
                }
            }
        }
    }
}
