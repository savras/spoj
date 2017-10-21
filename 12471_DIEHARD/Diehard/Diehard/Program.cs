using System;

namespace Diehard
{
    class Program
    {
        public static int Travel(int h, int a, bool canGoToAir)
        {
            var result = 0;
            if (canGoToAir)
            {
                result = Travel(h + 3, a + 2, false) + 1;
            }
            else
            {
                if ((h > 5 && a > 10) || h > 20 && a > 5)
                {
                    result = Math.Max(Travel(h - 5, a - 10, true), Travel(h - 20, a + 5, true)) + 1;
                }
            }
            return result;
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
                var result = Travel(h, a, true);
                Console.WriteLine(result);
            }
        }
    }
}
