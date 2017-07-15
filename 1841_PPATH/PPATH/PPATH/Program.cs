using System;
using System.Collections.Generic;

namespace PPATH
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = Convert.ToInt32(Console.ReadLine());

            for (var i = 0; i < n; i++)
            {
                var input = Console.ReadLine();
                var inputSplit = input.Split(' ');

                var oldNum = inputSplit[0];
                var newNum = inputSplit[1];

                var cost = 0;
                if (oldNum != newNum)
                {
                    var primes = new bool[10000];
                    for (var p = 1000; p < 10000; p++)
                    {
                        primes[p] = true;
                    }

                    SievePrimes(primes);
                    cost = Bfs(oldNum, newNum, primes);
                }
                
                Console.WriteLine(cost);
            }
        }

        private static int Bfs(string oldNum, string newNum, bool[] primes)
        {
            var cost = 1;
            var queue = new Queue<int>();
            var oldNumInt = int.Parse(oldNum);

            var visited = new HashSet<int>();

            queue.Enqueue(oldNumInt);
            queue.Enqueue(-1);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (current == -1)
                {
                    queue.Enqueue(-1);
                    cost++;
                    continue;
                }
                foreach (var n in GetNeighbour(primes, current))
                {
                    if (int.Parse(newNum) == n)
                    {
                        return cost;
                    }

                    if (!visited.Contains(n))
                    {
                        visited.Add(n);
                        queue.Enqueue(n);
                    }
                    
                }
            }
            return cost;
        }

        private static IEnumerable<int> GetNeighbour(bool[] primes, int current)
        {
            for (var candidate = 1000; candidate < 10000; candidate++)
            {
                if (primes[candidate] && candidate != current && IHasExactlyOneDigitDifferentFromCurrent(candidate, current))
                {
                    yield return candidate;
                }
            }
        }

        private static bool IHasExactlyOneDigitDifferentFromCurrent(int candidate, int current )
        {
            var diffCount = 0;
            var iString = candidate.ToString();
            var currentString = current.ToString();

            for(var i = 0; i < 4; i++)
            {
                if (iString[i] != currentString[i])
                {
                    diffCount++;
                }
            }
            return diffCount == 1;
        }

        static void SievePrimes(bool[] primes)
        {
            // Prepare prime list for 0 to 100
            var primesBefore100 = new bool[101];
            for (var i = 0; i <= 100; i++)
            {
                primesBefore100[i] = true;  // Initialization
            }

            var counter = 2;
            while (counter <= 100)
            {
                for (var i = counter * 2; i <= 100; i += counter)
                {
                    primesBefore100[i] = false;
                }
                counter = GetNextPrimeAfterCounter(primesBefore100, counter + 1);
            }

            // Primes for 1000 to 9999
            for (var i = 2; i <= 100; i++)
            {
                if (primesBefore100[i])
                {
                    var primeBefore1000ForI = (1000 / i) * i;

                    for (var p = primeBefore1000ForI; p < 10000; p += i)
                    {
                        primes[p] = false;
                    }
                }
            }
        }

        static int GetNextPrimeAfterCounter(bool[] primesBefore100, int counter)
        {
            for (var i = counter; i < 100; i++)
            {
                if (primesBefore100[i])
                {
                    return i;
                }
            }
            return 9999;
        }
    }
}