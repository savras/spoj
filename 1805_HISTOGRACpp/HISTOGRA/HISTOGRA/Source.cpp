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
* To calculate the area of the previous rectangle, we take the height of the previous rectangle "step",
* multiply by the start index of the previous rectangle up until the previous index. This gives us
* area = height of previous rectangle step * width of rectangle.
* Note: Notice that each step is recorded as we encounter an taller rectangle. This way, we know how far back
*		the width of the evaluated rectangle is.
*
* We need to do this for each item in the height stack that is larger than the height currently being assessed.
*/
#include <iostream>
#include <vector>
#include <stack>
#include <algorithm>

using std::endl;
using std::cin;
using std::cout;
using std::vector;
using std::stack;
using std::max;

int main()
{
	int t;
	cin >> t;
	while (t != 0) {
		vector<int> hist;
		for (size_t i = 0; i < t; i++) {
			int val;
			cin >> val;
			hist.push_back(val);
		}

		stack<int> startIndexStack;
		stack<int> heightStack;

		long long maxArea = 0;

		int i;
		for (i = 0; i < t; i++)
		{
			if (startIndexStack.size() == 0 || hist[i] > heightStack.top())
			{
				startIndexStack.push(i);
				heightStack.push(hist[i]);
			}
			else
			{
				while (heightStack.size() > 0 && heightStack.top() > hist[i])
				{
					int heightTop = heightStack.top();
					int startIndexTop = startIndexStack.top();

					startIndexStack.pop();
					heightStack.pop();

					int width = startIndexStack.size() == 0 ? i : i - startIndexStack.top() - 1;
					maxArea = max(maxArea, (long long)heightTop * (long long)width);					
				}

				startIndexStack.push(i);
				heightStack.push(hist[i]);
			}
		}


		while (heightStack.size() > 0)
		{
			int heightTop = heightStack.top();
			int startIndexTop = startIndexStack.top();

			startIndexStack.pop();
			heightStack.pop();

			int width = startIndexStack.size() == 0 ? i : i - startIndexStack.top() - 1;
			maxArea = max(maxArea, (long long)heightTop * (long long)width);
		}

		cout << maxArea << endl;

		cin >> t;
	}
	return 0;
}