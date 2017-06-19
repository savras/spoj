#include<iostream>
#include<vector>
#include<cmath>
#include<algorithm>
#include<limits>

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::max;

int getParent(const int& index) {
	return (index - 1) / 2;
}

int getLeftChild(const int& index) {
	return (2 * index) + 1;
}

int getRightChild(const int& index) {
	return (2 * index) + 2;
}

int populateInitialTree(const vector<int>& arr, vector<int>& tree, const int& start, const int& end, const int& current) {
	if (start == end) {
		tree[current] = arr[start];
	}
	else {
		int mid = start + ((end - start) / 2);
		int leftVal = populateInitialTree(arr, tree, start, mid, (2 * current) + 1);
		int rightVal = populateInitialTree(arr, tree, mid + 1, end, (2 * current) + 2);
		tree[current] = max(leftVal, rightVal);
	}	
	return tree[current];
}

void buildTree(vector<int>& arr, vector<int>& tree) {
	populateInitialTree(arr, tree, 0, arr.size() - 1, 0);
}

// Cater for these three scenarios:
// xi to yi completely covers the range of sum at tree[i]
//    then return value at tree[i]
// xi to yi partially covers the range covered at tree[i]
//    then traverse both left and right child
// xi to yi both do not fall into the range at tree[i]
//    then return a value that we wont change the result. E.g. if looking for sum then return 0.
long long findMax(const vector<int>& tree, const int& start, const int& end, const int& xi, const int& yi, const int& current) {
	if (start >= xi && end <= yi) {
		return tree[current];
	}
	else if (end < xi || start > yi) {
		return std::numeric_limits<std::int32_t>::max();
	}
	else {
		int mid = start + ((end - start) / 2);
		int leftVal = findMax(tree, start, mid, xi, yi, (2 * current + 1));
		int rightVal = findMax(tree, mid + 1, end, xi, yi, (2 * current + 2));
		return max(leftVal, rightVal);
	}
}

int main() {
	int n, m;
	int xi, yi;
	vector<int> arr;

	cin >> n;

	// -- The Algorithm Design Manual --
	// height = log n, where:
	// h is height
	// n = number of leaf nodes
	// log is log to base d, where d is the maximum number of children allowed per node.
	
	// The height of a perfectly balanced binary search tree, would be :
	// h = log m, where m is the total number of nodes.
	int height = (int)(ceil(log2(n)));	// Ceil gives number of nodes for perfectly balanced tree 

	// h = log m , therefore via log identity,
	// 2^h == total number of nodes in a tree of height h
	long long treeSize = 2 * (int)pow(2, height) - 1;

	vector<int> tree(treeSize);

	while (n--) {
		int val;
		cin >> val;
		arr.push_back(val);
	}
	buildTree(arr, tree);
	cin >> m;
	while (m--)
	{
		cin >> xi >> yi;
		long long result = findMax(tree, 0, arr.size() - 1, xi - 1, yi - 1, 0);
		cout << result << endl;
	}

	return 0;
}