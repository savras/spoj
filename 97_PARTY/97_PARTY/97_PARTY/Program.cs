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
                var dpArr = new int[count + 1, budget + 1];
                for (var i = 1; i <= count; i++) // Each party detail
                {
                    var priceOfPartyOfPartyI = parties[i - 1].Item1;
                    for (var j = priceOfPartyOfPartyI; j <= budget; j++)    // Price
                    {
                        var previousBestFunFactor = dpArr[i - 1, j];
                        var previousPartyFunFactorExcludingPartyI = dpArr[i - 1, j - priceOfPartyOfPartyI];
                        var funFactorAfterIncludingPartyI = previousPartyFunFactorExcludingPartyI + parties[i - 1].Item2;

                        dpArr[i, j] = Math.Max(previousBestFunFactor, funFactorAfterIncludingPartyI);
                    }
                }
                
                // Solve


                input = Console.ReadLine();
            }
        }
    }
}
