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
	index += index & -index;	// Add l.s.b. of index to index. E.g. 0100 + 0100 = 1000. E.g. 0101 + 0001 = 0110
	return index;
}

// 1) Perform two's complement on index
// 2) AND result with index
// 3) Subtract from index
// OR...
// 1) & index with index-1
// a.k.a. flip the l.s.b. bit with a value of 1
// e.g. 1111 & 1110 = 1110
int getParent(int index) {
	// Note: index & -index gives you the l.s.b. of index. E.g. 0011 & 0101 = 0001.
	// We then subtract that l.s.b. from index.
	// E.g. 0011 - 0001 = 0010
	// Doing index & (index - 1) essentially removes the l.s.b. of index.
	return index & (index - 1);	// return index -= index & -index;
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
		index = getNext(index);
	} while (index < bit.size());
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

// Return index of where a value is in relation to the original array.
// e.g. original array = {4, 2, 6, 5} and sorted = {2, 4, 5, 6}
// calling binarySearch for the value 4 will return 0, callling for value will return 3.
// We will end up with map = {1, 0, 3, 2}. E.g. value 2 in the original array is in position 1 (because map[0] == 1) of sorted array 
// Notice that we also add one to the result because our BIT starts at index 1.
// So we will finally end up with map = {2, 1, 4, 3}.
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

	for (int i = arr.size() - 1; i >= 0; i--) {
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