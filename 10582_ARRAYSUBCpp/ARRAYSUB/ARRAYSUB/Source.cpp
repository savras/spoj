// https://www.topcoder.com/community/data-science/data-science-tutorials/range-minimum-query-and-lowest-common-ancestor/
// Hints for Deque solution:
// https://stackoverflow.com/questions/12239042/spoj-arraysub-on-complexity-approach
// https://www.quora.com/How-can-I-solve-this-array-moving-window-max-problem-in-linear-time/answer/Akhilesh-Pandey-3
#include<iostream>
#include<vector>
#include<algorithm>
#include<cmath>
#include<deque>

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::ceil;
using std::log2;
using std::pow;
using std::max;
using std::deque;

void solveBruteForce(const vector<int>&, int, int);
void solveSegmentTree(const vector<int>&, int, int);
void buildTree(const vector<int>&, vector<int>&, const int&, const int&, const int&);
int solveST(const vector<int>&, const vector<int>&, const int&, const int&, const int&, const int&, const int&);
void solveDeque(const vector<int>&, const int&, const int&);

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
	
	solveDeque(arr, n, k);
	//solveSegmentTree(arr, n, k);
	//solveBruteForce(arr, n, k);
	return 0;
}

// Idea is to make the the largest element in k-window accessible from front of deque.
// Then, the back of the deque will have all values after the index of the largest value in the window.
// While we process new elements when sliding the window, we clear the back of the deque of any value smaller than the new element being processed.
// This way, we always have values in the deque in non-increasing order. Also notice that the values after the first element of the deque (from the front)
// is significant in finding the largest element in the window.
// Another thing to consider is that we don't pop the back of the deque if the new element considered is if the same value.
// This way we know that if the element that has just moved out of the window is of the very same one as the value front of the deque (in the case we have duplicates).
void solveDeque(const vector<int>& arr, const int& n, const int& k) {	
	deque<int> d;

	// Traverse the window and populate deque as per the description above.
	// Initially, we are making the deque track the growing of the window from size 0 to size k.
	for (size_t i = 0; i < k; i++) {
		while (!d.empty() && arr[i] > d.back()) {
			d.pop_back();
		}
		d.push_back(arr[i]);		
	}
	cout << d.front() << " ";
	
	// Now we slide the window.
	for (size_t i = 1; i <= n - k; i++) {
		// Remove old element
		if (arr[i - 1] == d.front()) {
			d.pop_front();
		}

		// Consider the new element.
		int nextElementIndex = i + k - 1;
		while (!d.empty() && arr[nextElementIndex] > d.back()) {
			d.pop_back();
		}
		d.push_back(arr[nextElementIndex]);

		cout << d.front() << " ";
	}
}

void solveSegmentTree(const vector<int>& arr, int n, int k) {
	// -- The Algorithm Design Manual --
	// height = log n, where:
	// h is height
	// n = number of nodes at tree height, h. (Or leaf nodes)
	// log is log to base d, where d is the maximum number of children allowed per node.

	// The height of a perfectly balanced (left and right levels differ by 1) binary search tree, would be:
	// h = log m, where m is the total number of nodes at the lowest level.
	int height = (int)(ceil(log2(n)));	// Ceil gives number of leaf nodes for perfectly balanced tree 

										// h = log m (for log base 2), therefore via log identity,
										// 2^h == m == total number of nodes in a tree of height h
	long long treeSize = 2 * (int)pow(2, height) - 1;	// Notice the number of nodes for height h - 1 is no. of leaf nodes at h - 1.
	vector<int> tree(treeSize);

	buildTree(arr, tree, 0, n - 1, 0);

	for (size_t i = 0; i <= n - k; i++) {
		cout << solveST(arr, tree, 0, n - 1, i, i + k - 1, 0) << " ";
	}
}
	
void buildTree(const vector<int>& arr, vector<int>& tree, const int& start, const int& end, const int& current) {
	if (start == end) {
		tree[current] = arr[start];
		return;
	}

	int leftChild = (2 * current) + 1;
	int rightChild = (2 * current) + 2;
	int mid = start + ((end - start) / 2);
	buildTree(arr, tree, start, mid, leftChild);
	buildTree(arr, tree, mid + 1, end, rightChild);

	tree[current] = max(tree[leftChild], tree[rightChild]);
}

int solveST(const vector<int>& arr, const vector<int>& tree, const int& s, const int& e, const int& l, const int& r, const int& current) {
	// node range fully overlapped by l and r
	if (s >= l && e <= r) {
		return tree[current];
	}
	if (s > r || e < l) {
		return -1;
	}

	int mid = s + ((e - s) / 2);
	int leftChild = (2 * current) + 1;
	int rightChild = (2 * current) + 2;
	int leftVal = solveST(arr, tree, s, mid, l, r, leftChild);
	int rightVal = solveST(arr, tree, mid + 1, e, l, r, rightChild);
	return max(rightVal, leftVal);
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