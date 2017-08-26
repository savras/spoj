/* Solution: Maintain 2 stacks; 
 * startIndex: Stores the index at which we encountered a height that is greater than the one we have
 *             encountered in the previous iteration.
 * height: Keeps a list of heights that we have encountered as we iterate through the heights of 
 *         the rectangles in the histogram.
 *         
 * We simply fill the start index and height in the stacks if we encounter a larger height.
 * When we encounter smaller heights, this is when we need to figure out the area of the rectangle at the previous 
 * index. 
 * 
 * To calculate the area of the previous rectangle, we take the height of the previous rectangle,
 * multiply by the start index of the previous rectangle up until the previous index. This gives us
 * area = height of previous rectangle * width of rectangle.
 * area = height.Peek() * (i - startIndex.Peek())
 * 
 * We need to do this for each item in the height stack that is larger than the height currently being assessed.
 */
using System;
using System.Collections.Generic;

namespace HISTOGRA
{
    class Program
    {
        static void Main(string[] args)
        {
            var line = string.Empty;
            do
            {
                line = Console.ReadLine();
                var split = line.Split(' ');
                var histogram = Array.ConvertAll(split, int.Parse);

                var startIndexStack = new Stack<int>();
                var heightStack = new Stack<int>();
                var maxArea = 0;

                int i;
                for (i = 1; i < histogram[0]; i++)
                {
                    if (startIndexStack.Count == 0 || histogram[i] > heightStack.Peek())
                    {
                        startIndexStack.Push(i);
                        heightStack.Push(histogram[i]);
                    }
                    else
                    {
                        while (heightStack.Count > 0 && heightStack.Peek() > histogram[i])
                        {
                            maxArea = Math.Max(maxArea, heightStack.Peek() * (i - startIndexStack.Peek()));
                            startIndexStack.Pop();
                            heightStack.Pop();
                        }
                    }
                }

                i++;
                while (heightStack.Count > 0)
                {
                    maxArea = Math.Max(maxArea, heightStack.Peek() * (i - startIndexStack.Peek()));
                    startIndexStack.Pop();
                    heightStack.Pop();
                }

                Console.WriteLine(maxArea);
            } while (line.Length > 1);
        }
    }
}
