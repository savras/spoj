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
                        var previousPartyFunFactorExcludingCurrentPartyItem = dpArr[i - 1, j - priceOfPartyOfPartyI];
                        var funFactorAfterIncludingCurrentItem = previousPartyFunFactorExcludingCurrentPartyItem + parties[i - 1].Item2;

                        dpArr[i, j] = Math.Max(previousBestFunFactor, funFactorAfterIncludingCurrentItem);
                    }
                }
                
                // Solve
                var partyToGoPrice = new int[count + 1];
                var currentBudget = budget;
                for (var i = count; i > 0; i--)
                {
                    var currentCount = i;
                    var currentBestFunFactor = dpArr[currentCount, currentBudget];

                    var currentPartyItemPrice = parties[i - 1].Item1;
                    var currentPartyItemFunFactor = parties[i - 1].Item2;

                    if (currentPartyItemPrice <= currentBudget)
                    {
                        var previousPartyFunFactorExcludingCurrentPartyItem = dpArr[currentCount - 1, currentBudget - currentPartyItemPrice];

                        if (Math.Abs(previousPartyFunFactorExcludingCurrentPartyItem - currentBestFunFactor) != currentPartyItemFunFactor)
                        {
                            partyToGoPrice[i] = 0;
                        }
                        else
                        {
                            partyToGoPrice[i] = 1;
                            currentBudget = currentBudget - currentPartyItemPrice;
                        }
                    }
                }

                var price = 0;
                for (var i = 1; i <= count; i++)
                {
                    if (partyToGoPrice[i] == 1)
                    {
                        price += parties[i - 1].Item1;
                    } 
                }
                Console.WriteLine(price + " " + dpArr[count, budget]);

                input = Console.ReadLine();
            }
        }
    }
}
