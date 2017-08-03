// Input issue: use RemoveEmptyEntries - https://msdn.microsoft.com/en-us/library/system.stringsplitoptions(v=vs.110).aspx
using System;
using System.Linq;

namespace ARRAYSUB
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var line = Console.ReadLine();
            var split = line.Split(' ').ToList().Select(int.Parse).ToList();
            var k = int.Parse(Console.ReadLine());

            var max = split[0];
            for (var i = 0; i <= n - k; i++)
            {
                for (var j = i; j < i + k; j++)
                {
                    if (max < split[j])
                    {
                        max = split[j];
                    }
                }
                Console.WriteLine(max);
            }
        }
    }
}
