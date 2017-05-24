#include<iostream>
#include<vector>

using std::cin;
using std::cout;
using std::endl;
using std::vector;

void buildSegmentTree(const vector<int>& arr, vector<int>& tree) {
	int arrSize = arr.size();
	int mid = (arrSize - 1) / 2;

	int treeSize = tree.size();
	int counter = treeSize - 1;
	for (int i = mid - 1; i >= 0; i--) {
		tree[counter] = arr[i];
		counter--;
	}

	for (int i = mid; i < arrSize - 1; i++) {
		tree[counter] = arr[i];
		counter--;
	}
}

int main() {
	int t, n, m;
	int xi, yi;
	vector<int> arr;
	
	cin >> t;
	while (t--) {
		cin >> n;
		vector<int> tree((2 * n) - 1);

		while (n--) {
			cin >> n;
			arr.push_back(n);
		}
		cin >> xi >> yi;
		buildSegmentTree(arr, tree);
	}	
	return 0;
}