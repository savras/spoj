using System;
using System.Collections.Generic;

namespace LASTDIG
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<int, List<int>>();
            var t = int.Parse(Console.ReadLine());
            for (var i = 0; i < t; i++)
            {
                var line = Console.ReadLine();
                var split = line.Split(' ');

                var baseNo = int.Parse(split[0]);
                var power = int.Parse(split[1]);

                if (baseNo == 0)
                {
                    Console.WriteLine(0);
                }
                if (power == 0)
                {
                    Console.WriteLine(1);
                }
                else
                {
                    PopulateDictionary(baseNo, dict);
                    Console.WriteLine(dict[baseNo][(power - 1) % 4]);
                }
            }
        }

        public static void PopulateDictionary(int baseNo, Dictionary<int, List<int>> dictionary)
        {
            if (!dictionary.ContainsKey(baseNo))
            {
                // My limited tests show that the last numbers repeat themselves at most circularly every 4 times
                // E.g. 3^1 == 3, e^2 == 9, 3^3 == 7, 3^4 == 1, and the list repeats 3->9->7->1.
                // Other base numbers are less so 4 should be enough to cover the full repetition.
                dictionary.Add(baseNo, new List<int>());
                for (var i = 1; i <= 4; i++)
                {
                    var lastDigit = (int) Math.Pow(baseNo, i)%10;
                    dictionary[baseNo].Add(lastDigit);
                }
            }
        }
    }
}
