#include <iostream>
#include <vector>
#include <string>

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::string;

// BIT/Fenwick implementation
// ToDo:: On hold - to be completed when I understand how to do this.
int getNext(const int& index) {
	return index + (index & (~index + 1));
}

int getParent(const int& index) {
	return index & (index - 1);	
}

long long getAt(const vector<int>& arr, const int& index) {
	long long result = 0;
	int current = index;
	do {
		result += arr[current];
		current = getParent(current);
	} while (current > 0);

	return result;
}

void update(vector<int>& arr, const int& index, const int& value) {
	int current = index;
	do {
		arr[current] += value;
		current = getNext(current);
	} while (current < arr.size());
}

// Segment tree implementation
long long update(vector<int>& arr, vector<int>& lazy, const int& s, const int& e, 
	const int& l, const int& r, const int& value, const int& current) {

	int leftChild = (2 * current) + 1;
	int rightChild = (2 * current) + 2;


	if ((l < s  && r > e) || (s < l && e < l) || (s > r && e > r)) {
		if (lazy[current] != 0) {
			if (s != e) {
				int mid = s + ((e - s) / 2);
				arr[current] += ((e - s + 1) * lazy[current]);
				lazy[leftChild] += lazy[current];
				lazy[rightChild] += lazy[current];
			}
			else {
				arr[current] = lazy[current];
			}

			lazy[current] = 0;
		}
		return arr[current];
	}

	if (s >= l && e <= r) {		
		if (lazy[current] != 0) {
			arr[current] += lazy[current];
			lazy[current] = 0;
		}

		if (s != e) {	// Has children
			int mid = s + ((e - s) / 2);
			arr[current] += ((e - s + 1) * value);
			lazy[leftChild] += value;			
			lazy[rightChild] += value;
		}
		else {
			arr[current] += value;
		}

		return arr[current];
	}

	if (lazy[current] != 0)
	{
		if (s != e) {	// Has children
			int mid = s + ((e - s) / 2);
			lazy[leftChild] += lazy[current];
			lazy[rightChild] += lazy[current];
			lazy[current] = 0;
		}
	}

	int mid = s + ((e - s) / 2);
	long long leftVal = update(arr, lazy, s, mid, l, r, value, leftChild);
	long long rightVal = update(arr, lazy, mid + 1, e, l, r, value, rightChild);	
	
	arr[current] = leftVal + rightVal;

	return arr[current];
}

long long getSum(vector<int>& arr, vector<int> lazy, const int& s, const int& e, const int& l, const int& r, const int& current) {
	if (l <= s && r >= e) {
		if (lazy[current] != 0) {
			int leftChild = (2 * current) + 1;
			int rightChild = (2 * current) + 2;

			if (s != e) {
				lazy[leftChild] += lazy[current];
				lazy[rightChild] += lazy[current];
			}

			arr[current] += lazy[current];
			lazy[current] = 0;
		}
		return arr[current];
	}

	if ((s < l && e > r) || (s < l && e < l) || (s > r && e > r)) {
		if (lazy[current] != 0) {
			int leftChild = (2 * current) + 1;
			int rightChild = (2 * current) + 2;

			if (s != e) {
				lazy[leftChild] += lazy[current];
				lazy[rightChild] += lazy[current];
			}

			arr[current] += lazy[current];
			lazy[current] = 0;
		}
		return 0;
	}

	int mid = s + (e - s) / 2;
	long long leftVal = getSum(arr, lazy, s, mid, l, r, (2 * current) + 1);
	long long rightVal = getSum(arr, lazy, mid + 1, e, l, r,  2 * current + 2);
	return leftVal + rightVal;
}

int main() {
	int t;
	int n, c, code, l, r, value;

	cin >> t;
	while (t--) {
		cin >> n;
		
		int height = (int)(ceil(log2(n)));										
		long long treeSize = 2 * (int)pow(2, height) - 1;

		vector<int> arr(treeSize);
		vector<int> lazy(treeSize);
		
		cin >> c;
		while (c--) {
			cin >> code;
			if (code == 0) {
				cin >> l >> r >> value;
				update(arr, lazy, 0, n - 1, l - 1, r - 1, value, 0);
			}
			else {
				cin >> l >> r;
				cout << getSum(arr, lazy, 0, n - 1, l - 1, r - 1, 0) << "\n";

				//long long result = getAt(arr, yi) - getAt(arr, xi - 1);
				//cout << result << endl;
			}
		}
	}

	return 0;
}