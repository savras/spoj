#include "iostream"
#include "queue"
#include "vector"

using std::cin;
using std::cout;
using std::endl;
using std::queue;
using std::vector;

void merge(vector<int>& arr, const int& start, const int&end, const int& mid) {
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

void mergesort(vector<int>& arr, const int& start, const int& end) {
	if (start < end) {
		int mid = start + (end - start) / 2;
		mergesort(arr, start, mid);
		mergesort(arr, mid + 1, end);

		merge(arr, start, end, mid);
	}	
}

int main() {
	int t, n, p;
	cin >> t;

	while (t--) {
		cin >> n;
		vector<int> arr;
		for (size_t i = 0; i < n; i++) {
			cin >> p;
			arr.push_back(p);
		}

		mergesort(arr, 0, arr.size() - 1);
	}

	return 0;
}