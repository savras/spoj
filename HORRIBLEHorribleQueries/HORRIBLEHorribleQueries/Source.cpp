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
int main() {
	int t;
	int n, c, code, xi, yi, valu me;

	cin >> t;
	while (t--) {
		cin >> n;
		vector<int> arr(n + 1);

		cin >> c;
		while (c--) {
			cin >> code;
			if (code == 0) {
				cin >> xi >> yi >> value;
				update(arr, xi, value);
			}
			else {
				cin >> xi >> yi;
				long long result = getAt(arr, yi) - getAt(arr, xi - 1);
				cout << result << endl;
			}
		}
	}

	return 0;
}