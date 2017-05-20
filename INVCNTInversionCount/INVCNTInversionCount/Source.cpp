#include "iostream"
#include "queue"
#include "vector"
#include "algorithm"

using std::cin;
using std::cout;
using std::endl;
using std::queue;
using std::vector;
using std::sort;


/*
 * Mergsort solution
 */
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

/*
 * Binary index tree solution
 */

 // 1) Perform two's complement on index
 // 2) AND result with index
 // 3) Add to index
int getNext(int index) {
	return ((~index + 1) & index) + index;
	// ToDo:: Change to return index & -index;
}

// 1) Perform two's complement on index
// 2) AND result with index
// 3) Subtract from index
// OR...
// 1) & index with index-1
// a.k.a. flip l.s.b. bit with value 1
int getParent(int index) {
	int x = index & -index;
	return index & (index - 1);
}

void createbinaryIndexedTree(const vector<int>& arr, vector<int>& bit) {
	for (size_t i = 0; i < arr.size(); i++) {
		int store = arr[i];
		int counter = i + 1;
		do {
			bit[counter] += store;
			counter = getNext(counter);
		} while (counter < bit.size());
	}
}

long long getSumAtIndex(const vector<int>& bit, int index) {
	long long sum = bit[index];

	while (index > 0) {
		index = getParent(index);
		sum += bit[index];
	}

	return sum;
}

void updateAtIndex(vector<int>& bit, int index, const int& value)
{
	do {
		bit[index] += value;
		index = getParent(index);
	} while (index > 0);
}

long long getRunningSum(const vector<int>& arr, int index) {
	long long sum = 0;
	int counter = index;
	while (counter > 0) {
		sum += arr[counter];
		counter = getParent(counter);
	}

	return sum;
}

int binarySearch(const vector<int>& sorted, const int& value, const int& start, const int &end) {
	int result = -1;
	if (start <= end) {		
		int mid = start + (end - start) / 2;

		if (sorted[mid] == value) {
			result = mid;
		}
		else if (sorted[mid] > value) {
			result = binarySearch(sorted, value, start, mid - 1);
		}
		else
		{
			result = binarySearch(sorted, value, mid + 1, end);
		}
	}
	return result;
}
int getMappedValue(const int& value, const vector<int>& sorted) {
	return binarySearch(sorted, value, 0, sorted.size() - 1);
}

void binaryIndexTree(const vector<int>& arr, vector<int>& bit, long long& result) {
	//createbinaryIndexedTree(arr, bit);
	
	vector<int> sorted(arr);
	sort(sorted.begin(), sorted.end());

	vector<int> map(arr.size());
	for (size_t i = 0; i < arr.size(); i++) {
		map[i] = getMappedValue(arr[i], sorted) + 1;
	}

	for (int i = arr.size() - 1; i > 0; i--) {
		result += getSumAtIndex(bit, map[i]);
		updateAtIndex(bit, map[i], 1);
	}
}

int main() {
	int t, n, p;
	cin >> t;
	
	while (t--) {
		cin >> n;
		vector<int> arr;
		vector<int> bit(n + 1);
		long long result = 0;
		for (size_t i = 0; i < n; i++) {
			cin >> p;
			arr.push_back(p);
		}		

		// mergesort(arr, 0, arr.size() - 1, result);
		binaryIndexTree(arr, bit, result);

		cout << result << endl;		
	}
	
	return 0;
}