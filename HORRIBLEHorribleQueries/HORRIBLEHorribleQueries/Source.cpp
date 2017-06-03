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

	if (l < s  && r > e) {
		return 0;
	}

	if (s >= l && e <= r) {
		if (lazy[current] != 0) {
			arr[current] += lazy[current];
			lazy[current] = 0;

			if (leftChild <= lazy.size() - 1)
			{
				lazy[leftChild] += value;
			}
			if (rightChild <= lazy.size() - 1)
			{
				lazy[rightChild] += value;
			}
		}
		arr[current] += value;
		return arr[current];
	}

	int mid = s + ((e - s) / 2);
	long long leftVal = update(arr, lazy, s, mid, l, r, value, leftChild);
	long long rightVal = update(arr, lazy, mid + 1, e, l, r, value, rightChild);
	
	if (lazy[current] != 0)
	{
		arr[current] += lazy[current];
		lazy[current] = 0;		
	}
	return arr[current];
}

long long getValue(vector<int>& st, vector<int> lazy, const int& s, const int& e, const int& l, const int& r, const int& current) {
	if (l <= s && r >= e) {
		return st[current];
	}

	if (s < l && e > r) {
		return 0;
	}

	int mid = s + (e - s) / 2;
	long long leftVal = getValue(st, lazy, s, mid, l, r, (2 * current) + 1);
	long long rightVal = getValue(st, lazy, mid + 1, e, l, r,  2 * current + 2);
	return leftVal + rightVal;
}

long long buildSegmentTree(const vector<int>& arr, vector<int>& st, const int& s, const int& e, const int& current) {
	if (s == e)
	{
		st[current] = arr[s];
		return st[current];
	}

	int mid = s + (e - s) / 2;
	long long leftVal = buildSegmentTree(arr, st, s, mid, (2 * current) + 1);
	long long rightVal = buildSegmentTree(arr, st, mid + 1, e, (2 * current) + 2);
	st[current] = leftVal + rightVal;

	return st[current];
}

int main() {
	int t;
	int n, c, code, xi, yi, value;

	cin >> t;
	while (t--) {
		cin >> n;
		vector<int> arr(n);
		vector<int> lazy(n);
		
		cin >> c;
		while (c--) {
			cin >> code;
			if (code == 0) {
				cin >> xi >> yi >> value;
				update(arr, lazy, 0, n - 1, xi, yi, value, 0);
			}
			else {
				cin >> xi >> yi;


				//long long result = getAt(arr, yi) - getAt(arr, xi - 1);
				//cout << result << endl;
			}
		}
	}

	return 0;
}