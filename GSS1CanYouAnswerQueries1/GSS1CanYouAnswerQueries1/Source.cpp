#include<iostream>
#include<vector>
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

void populateInitialTree(const vector<int>& arr, vector<int>& tree, const int& start, const int& end, const int& current) {
	if (start == end) {
		tree[current] = arr[start];
		return;
	}

	int mid = start + ((end - start) / 2);
	populateInitialTree(arr, tree, start, mid, (2 * current) + 1);
	populateInitialTree(arr, tree, mid + 1, end, (2 * current) + 2);
}

void calcParent(vector<int>& tree) {
	int treeSize = sizeof(tree) / sizeof(int);
	for (int i = treeSize - 1; i > 0; i -= 2) {
		int p = getParent(i);
		tree[p] = max(tree[i], tree[i - 1]);
	}
}

void buildTree(vector<int>& arr, vector<int>& tree) {
	populateInitialTree(arr, tree, 0, arr.size() - 1, 0);
	calcParent(tree);
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
		return INT_MIN;
	}
	else {
		int mid = start + ((end - start) / 2);
		return max(findMax(tree, start, mid, xi, yi, (2 * current + 1)), findMax(tree, mid + 1, end, xi, yi, (2 * current + 2)));
	}
}

int main() {
	int n, m;
	int xi, yi;
	vector<int> arr;

	cin >> n;
	int treeSize = ((2 * n) - 1);
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
		long long result = findMax(tree, 0, arr.size() - 1, xi, yi, 0);
		cout << result << endl;
	}

	return 0;
}