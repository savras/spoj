using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLOLO
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var hs = new HashSet<int>();
            var sum = 0;
            for (var i = 0; i < n; i++)
            {
                var val = int.Parse(Console.ReadLine());
                if (hs.Contains(val))
                {
                    sum -= val;
                }
                else
                {
                    sum += val;
                    hs.Add(val);
                }
            }

            Console.WriteLine(Math.Abs(sum));
        }
    }
}
