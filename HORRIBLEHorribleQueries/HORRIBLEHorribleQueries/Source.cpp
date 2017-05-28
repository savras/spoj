#include <iostream>
#include <vector>
#include <string>

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::string;

// BIT/Fenwick implementation
int getNext(const int& index) {
	return index & (index - 1);
}

int getParent(const int& index) {
	return index + (~index);
}

long long getAt(const vector<int>& arr, const int& index) {
	long long result = 0;
	int parent = getParent(index);
	while (parent > 0) {
		result += arr[parent];
	}

	return result;
}

void update(vector<int>& arr, const int& index, const int& value) {
	int next = getNext(index);
	while (next < arr.size()) {
		arr[next] += value;
	}
}

// Segment tree implementation

int main() {
	int t;
	int n, c, code, xi, yi, value;

	cin >> t;
	while (t--) {
		cin >> n;
		while (n--) {
			vector<int> arr(n);
			cin >> c;
			while (c--) {
				cin >> code >> xi >> yi >> value;
				if (code == 1) {
					update(arr, xi, value);
				}
				else {

				}
			}
		}
	}

	return 0;
}