#include <iostream>
#include <vector>

using std::cin;
using std::vector;
using std::ios_base;

/*
Prefix sum solution:
for each update:
arr[l] += value;
arr[r + 1] -= value;

build prefix sum from arr.

profit!
*/

int getParent(const int& index) {
	return index - (index & (-index));
}

int getNext(const int& index) {
	return index + (index & (-index));
}

long long getAt(const int& index, vector<long long>& arr) {
	int current = index;
	long long result = 0;
	do {
		result += arr[current];
		current = getParent(current);
	} while (current > 0);

	return result;
}

void update(vector<long long>& arr, const int& index, const int& value) {
	int current = index;
	do {
		arr[current] += value;
		current = getNext(current);
	} while (current < arr.size());
}

void prebuildArray(vector<long long>& arr, const int& l, const int& r, const int& val) {
	update(arr, l, val);
	update(arr, r + 1 , -val);
}

int main() {
	int t;
	scanf("%d", &t);

	int n, u, l, r, val;
	int q, p;

	while (t--) {
		scanf("%d%d", &n, &u);
		vector<long long> arr(n + 2);
		while (u--) {			
			scanf("%d%d%d", &l, &r, &val);
			prebuildArray(arr, l + 1, r + 1, val);
		}
		scanf("%d", &q);
		while (q--) {
			scanf("%d", &p);			
			printf("%lld\n", getAt(p + 1, arr));
		}
	}
}