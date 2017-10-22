using System;

namespace Diehard
{
    class Program
    {
        public static int Travel(int h, int a, bool canGoToAir, int[,] cache)
        {
            if (h < 0 || a < 0)
            {
                return 0;
            }

            if (cache[h, a] >= 0)
            {
                return cache[h, a];
            }

            cache[h, a] = 0;
            if (canGoToAir)
            {
                cache[h, a] = Travel(h + 3, a + 2, false, cache) + 1;
            }
            else
            {
                if ((h > 5 && a > 10) || h > 20 && a > 5)
                {
                    // Note that air path can be combined with this and return + 2 step instead.
                    cache[h, a] = Math.Max(Travel(h - 5, a - 10, true, cache), Travel(h - 20, a + 5, true, cache)) + 1;
                }
            }

            return cache[h, a];
        }
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());

            for (var i = 0; i < t; i++)
            {
                var line = Console.ReadLine();
                var split = line.Split(' ');
                var h = int.Parse(split[0]);
                var a = int.Parse(split[1]);

                // Assuming h = 1000 and a = 1000,
                // Largest a = 1000/17 * 7 = 412, where we go by -20 health every time while increasing 7 armor
                // Largest h = n + 3, where we increase health by 3 the very first time. Subsequent increases will never go past n + 3
                var cache = new int[h + 3 + 1, a + 412 + 1];    

                cache[0, 0] = 0;
                for (var p = 0; p < h + 3 + 1; p++)
                {
                    for (var j = 0; j < a + 412 + 1; j++)
                    {
                        cache[p, j] = -1;
                    }
                }

                // Precompute picking of air first
                h += 3;
                a += 2;
                var result = Travel(h, a, false, cache) + 1;
                Console.WriteLine(result);
            }
        }
    }
}
