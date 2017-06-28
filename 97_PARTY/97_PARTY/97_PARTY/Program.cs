using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _97_PARTY
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitString = "0 0";
            var input = Console.ReadLine();

            while (input != exitString)
            {
                var inputSplit = input.Split(' ');
                var budget = Convert.ToInt32(inputSplit[0]);
                var count = Convert.ToInt32(inputSplit[1]);

                var parties = new List<Tuple<int, int>>();
                for (var i = 0; i < count; i++)
                {
                    var party = Console.ReadLine();
                    var partySplit = party.Split(' ');
                    parties.Add(new Tuple<int, int>(Convert.ToInt32(partySplit[0]), Convert.ToInt32(partySplit[1])));
                }

                // Build 2d Array
                var dpArr = new int[count, budget];
                for (var i = 1; i < count; i++)
                {
                    for (var j = 1; j < budget; j++)
                    {
                        var top = dpArr[i - 1, j];
                        var left = dpArr[i, j - 1];
                        var value = Math.Max(top, left);
                        if (j%parties[i].Item1 == 0)
                        {
                            value = Math.Max(value, parties[i].Item1);
                        }
                        dpArr[i, j] = value;
                    }
                }
                
                // Solve



                input = Console.ReadLine();
            }
        }
    }
}
