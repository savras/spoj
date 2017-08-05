// https://www.topcoder.com/community/data-science/data-science-tutorials/range-minimum-query-and-lowest-common-ancestor/
// Hints for Deque solution:
// https://stackoverflow.com/questions/12239042/spoj-arraysub-on-complexity-approach
// https://www.quora.com/How-can-I-solve-this-array-moving-window-max-problem-in-linear-time/answer/Akhilesh-Pandey-3
#include<iostream>
#include<vector>

using std::cin;
using std::cout;
using std::endl;
using std::vector;

void solveBruteForce(const vector<int>&, int, int);

// ToDo:: Solve using BIT and Deque
int main() {
	int n;
	cin >> n;

	vector<int> arr;
	int value;
	for (size_t i = 0; i < n; i++) {
		cin >> value;
		arr.push_back(value);
	}

	int k;
	cin >> k;
	
	//solveSegmentTree(arr, n, k);
	solveBruteForce(arr, n, k);
	return 0;
}

void solveBruteForce(const vector<int>& arr, int n, int k) {
	int max;
	for (size_t i = 0; i <= n - k; i++)
	{
		max = arr[i];
		for (size_t j = i; j < i + k; j++)
		{
			if (max < arr[j])
			{
				max = arr[j];
			}
		}
		cout << max << " ";
	}
}