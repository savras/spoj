using System;
using System.Collections.Generic;

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

                var parties = new List<Tuple<int, int>> {new Tuple<int, int>(0, 0)};
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
                    var priceOfCurrentPartyItem = parties[i].Item1;
                    for (var j = 1; j <= budget; j++) // Price
                    {
                        var previousBestFunFactor = dpArr[i - 1, j];
                        var funFactorAfterIncludingCurrentItem = 0;
                        if (j >= priceOfCurrentPartyItem)
                        {
                            var previousPartyFunFactorExcludingCurrentPartyItem = dpArr[i - 1, j - priceOfCurrentPartyItem];
                            funFactorAfterIncludingCurrentItem = previousPartyFunFactorExcludingCurrentPartyItem + parties[i].Item2;
                        }

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

                    var priceOfCurrentPartyItem = parties[i].Item1;
                    var funFactorOfCurrentPartyItem = parties[i].Item2;

                    if (priceOfCurrentPartyItem <= currentBudget)
                    {
                        var previousPartyFunFactorExcludingCurrentPartyItem = dpArr[currentCount - 1, currentBudget - priceOfCurrentPartyItem];

                        if (Math.Abs(previousPartyFunFactorExcludingCurrentPartyItem - currentBestFunFactor) != funFactorOfCurrentPartyItem)
                        {
                            partyToGoPrice[i] = 0;
                        }
                        else
                        {
                            partyToGoPrice[i] = 1;
                            currentBudget = currentBudget - priceOfCurrentPartyItem;
                        }
                    }
                }

                var price = 0;
                for (var i = 1; i <= count; i++)
                {
                    if (partyToGoPrice[i] == 1)
                    {
                        price += parties[i].Item1;
                    } 
                }
                Console.WriteLine(price + " " + dpArr[count, budget]);

                input = Console.ReadLine();
            }
        }
    }
}
