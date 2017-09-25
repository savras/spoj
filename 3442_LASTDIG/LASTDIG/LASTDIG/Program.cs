// SPOJ wants the file to be at most 700 bytee, see the hard to read version below.
using System;
using System.Collections.Generic;

namespace LASTDIG
{
    class Program
    {
        static void Main()
        {
            var dict = new Dictionary<int, List<int>>();
            var t = int.Parse(Console.ReadLine());
            for (var i = 0; i < t; i++)
            {
                var line = Console.ReadLine();
                var split = line.Split(' ');
                var baseNo = int.Parse(split[0]);
                var power = int.Parse(split[1]);
                var result = 1;

                if (power > 0)
                {
                    if (!dict.ContainsKey(baseNo))
                    {
                        dict.Add(baseNo, new List<int>());

                        // My limited tests show that the last numbers repeat themselves at most circularly every 4 times
                        // E.g. 3^1 == 3, e^2 == 9, 3^3 == 7, 3^4 == 1, and the list repeats 3->9->7->1.
                        // Other base numbers are less so 4 should be enough to cover the full repetition.
                        for (var j = 1; j <= 4; j++)
                        {
                            var lastDigit = (int) Math.Pow(baseNo, j)%10;
                            dict[baseNo].Add(lastDigit);
                        }
                    }
                    result = dict[baseNo][(power - 1)%4];
                }
                Console.WriteLine(result);
            }
        }
    }
}

// SPOJ submission:
/*
using System; using System.Collections.Generic;
namespace LASTDIG { class Program { static void Main() {
var dict = new Dictionary<int, List<int>>(); var t = int.Parse(Console.ReadLine());
for (var i = 0; i < t; i++) {
var line = Console.ReadLine();var split = line.Split(' ');var baseNo = int.Parse(split[0]);var power = int.Parse(split[1]);var result = 1;
if (power > 0) {
if (!dict.ContainsKey(baseNo)) {
dict.Add(baseNo, new List<int>());
for (var j = 1; j <= 4; j++) {
var lastDigit = (int)Math.Pow(baseNo, j) % 10;dict[baseNo].Add(lastDigit);
}}
result = dict[baseNo][(power - 1) % 4];
}Console.WriteLine(result);}}}}
*/