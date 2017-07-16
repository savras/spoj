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
                    for (var p = 0; p < 10000; p++)
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
            queue.Enqueue(-1);  // Track current level of BFS

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (current == -1)
                {
                    queue.Enqueue(-1);  // Enqueue a -1 to mark that we have finished traversing one level in BFS
                    cost++;
                    continue;
                }
                foreach (var n in GetNeighbour(primes, current))
                {
                    if (n != -1)
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
            }
            return cost;
        }


        // Increment the the digits in 'current' one by one and return it.
        static IEnumerable<int> GetNeighbour(bool[] primes, int current)
        {
            for (var i = 1000; i > 0; i /= 10)
            {
                var magnitude = i;
                var baseValue = RemoveMagnitude(magnitude, current);
                for (var j = 0; j <= 9; j++)
                {
                    var candidatePrime = baseValue + (magnitude * j);
                    if (candidatePrime >= 1000 && primes[candidatePrime] && candidatePrime != current)
                    {
                        yield return candidatePrime;
                    }
                }
            }
        }

        static int RemoveMagnitude(int magnitude, int current)
        {
            // E.g. magnitude = 100:
            var rightOfMagnitude = current% magnitude;      // 7912 % 100 = 12
            var leftOfMagnitude = current/magnitude;        // 7912 / 100 = 79
            var rightmostDigit = leftOfMagnitude%10;        //    79 % 10 = 9

            // ((70 - 9) * 100) + 12 == 7012
            var result = ((leftOfMagnitude - rightmostDigit)*magnitude) + rightOfMagnitude;

            return result;
        }

        // Get a list of prime numbers from 2 to sqrt(10000)
        static void SievePrimes(bool[] primes)
        {
            for (var i = 2; i <= 100; i++)
            {
                if (primes[i])
                {
                    for (var j = i * 2; j < 10000; j += i)
                    {
                        primes[j] = false;
                    }
                }
            }
        }
    }
}