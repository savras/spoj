#include "iostream"
#include "queue"
#include "vector"

using std::cin;
using std::cout;
using std::endl;
using std::queue;
using std::vector;

// 1) Perform two's complement on itself
// 2) AND with itself
// 3) Add to itself
int getNext(int index) {
	return ((~index + 1) & index) + index;
}

// 1) Perform two's complement on itself
// 2) AND with itself
// 3) Subtract from original number
// OR...
// 1) & index with index-1
// a.k.a. flip l.s.b. bit with value 1
int getParent(int index) {
	return index & (index - 1);
}

void createbinaryIndexTree(vector<int>& arr) {
	for (size_t i = 1; i < arr.size(); i++) {
		int store = arr[i - 1];
		arr[i] += store;

		int counter = getNext(i);
		while (counter <= arr.size()) {
			arr[counter] += store;
		}
	}

}

void merge(vector<int>& arr, const int& start, const int&end, const int& mid, long long & result) {
	queue<int> left, right;

	for (size_t i = start; i <= mid; i++) {
		left.push(arr[i]);
	}

	for (size_t i = mid + 1; i <= end; i++) {
		right.push(arr[i]);
	}

	int i = start;
	while (!left.empty() && !right.empty()) {
		if (left.front() < right.front()) {
			arr[i] = left.front();
			left.pop();
		}
		else {
			result += left.size();
			arr[i] = right.front();
			right.pop();
		}
		i++;
	}

	while (!left.empty()) {
			arr[i++] = left.front();
			left.pop();		
	}

	while (!right.empty()) {
		arr[i++] = right.front();
		right.pop();
	}
}

void mergesort(vector<int>& arr, const int& start, const int& end, long long & result) {
	if (start < end) {
		int mid = start + (end - start) / 2;
		mergesort(arr, start, mid, result);
		mergesort(arr, mid + 1, end, result);

		merge(arr, start, end, mid, result);
	}	
}

int main() {
	int t, n, p;
	cin >> t;

	
	while (t--) {
		cin >> n;
		vector<int> arr;
		long long result = 0;
		for (size_t i = 0; i < n; i++) {
			cin >> p;
			arr.push_back(p);
		}		
		// mergesort(arr, 0, arr.size() - 1, result);

		cout << result << endl;		
	}
	
	return 0;
}