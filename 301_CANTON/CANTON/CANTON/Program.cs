// Idea: Use triangular numbers.
// Reason: Notice that the numbers go from 1 to c in a zig zag manner. This means
// that all the numbers from 1 to c will be in the top left part of the triangle.
// Triangular number's formula, 2 * c = n + (n + 1) will give us the filled diagonal of the triangle.
// Using the value of the triangle from the formula, we can deduct c from it.

using System;

namespace CANTON
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());

            for (var i = 0; i < t; i++)
            {
                var term = int.Parse(Console.ReadLine());

                Solve(term);
            }
        }

        private static void Solve(int term)
        {
            var c = term*2;

            // Solve for n^2 + n = 2 * term using quadratic formula
            var valueToSquareRoot = 1 - (4*(c * -1));
            var squareRootResult = Math.Sqrt(valueToSquareRoot);
            var n = Math.Ceiling(Math.Abs((1 - squareRootResult) / 2));

            // Using the triangular formula, we get the largest value that we can get when we fill the triangle.
            var largestValueForTriangle = (int)(Math.Pow(n, 2) + n)/2;

            
            // ToDo: Solve this later.
            Console.WriteLine(largestValueForTriangle);

            // Console.WriteLine("TERM {0} IS {1} / {2}", term, rowNumber, colNumber);
        }
    }
}

/*
#include<stdio.h>
#include<math.h>
int main()
{
    long long int n , x ,i , j ,sub , sum , t;
    
    scanf("%lld",&t);
    while(t--)
    {
        scanf("%lld",&x);
        n=ceil((-1+sqrt(1+8*x))/2);
        sub = x - n*(n-1)/2;
        sum = n+1;
        if (n%2==0)
            printf("TERM %lld IS %lld/%lld\n",x , sub , sum-sub);    
        else
            printf("TERM %lld IS %lld/%lld\n",x, sum -sub ,sub);            
    }
    return 0;
}

 */
