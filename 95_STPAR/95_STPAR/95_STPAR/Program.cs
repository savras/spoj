using System;
using System.Collections.Generic;

namespace _95_STPAR
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());
            while (t != 0)
            {
                var inputLine = Console.ReadLine();
                var inputSplit = inputLine.Split(' ');

                var q = new Queue<int>();
                for (var i = 0; i < t; i++)
                {
                    q.Enqueue(Convert.ToInt32(inputSplit[i]));
                }

                var counter = 1;
                var s = new Stack<int>();
                while (q.Count != 0 || s.Count != 0)
                {
                    if ((q.Count != 0 && q.Peek() == counter) || (s.Count != 0 && s.Peek() == counter))
                    {
                        if (s.Count != 0 && s.Peek() == counter)
                        {
                            s.Pop();
                        }
                        else
                        {
                            q.Dequeue();
                        }
                        counter++;
                    }
                    else
                    {
                        if (q.Count != 0)
                        {
                            var value = q.Peek();
                            q.Dequeue();

                            if (s.Count != 0)
                            {
                                var sentinel = s.Peek();
                                if (sentinel < value)
                                {
                                    break;
                                }
                            }
                            s.Push(value);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                Console.WriteLine(s.Count == 0 && q.Count == 0 ? "yes" : "no");

                t = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
