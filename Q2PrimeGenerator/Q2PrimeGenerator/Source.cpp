#include <iostream>
#include <cmath>
#include <vector>

using std::cin;
using std::cout;
using std::endl;
using std::vector;

void buildPrimes(vector<int>& primes, int size) {
	primes.push_back(1);
	primes.push_back(2);
	primes.push_back(3);
	bool insert;

	for (size_t number = 4; number <= size; number++) {
		int sqrroot = sqrt(number);
		insert = true;
		for (size_t p = 1; p <= primes.size() - 1; p++) {
			if (primes[p] > sqrroot) { break; }
			if (number % primes[p] == 0) {
				insert = false;
				break;
			}
		}
		if (insert) {
			primes.push_back(number);
		}
	}
}

int binarySearch(const vector<int>& primes, int start, int end, int target) {
	if (start >= end) {
		return start;	
	}

	int mid = start + ((end - start) / 2);

	if (primes[mid] > target) {
		binarySearch(primes, start, mid, target);
	}
	else if (primes[mid] < target) {
		binarySearch(primes, mid + 1, end, target);
	}
	else {
		return mid;
	}
}

int main() {
	int t;
	cin >> t;

	int size = sqrt(1000000000);
	vector<int> primes;
	buildPrimes(primes, size);

	int m, n;
	for (size_t p = 0; p < t; p++) {
		cin >> m >> n;
		int index = binarySearch(primes, 0, primes.size() - 1, m);

		if (primes[index] < p) {
			index++;
		}

		while (primes[index] <= n) {
			cout << primes[index] << endl;
			index++;
		}
	}	
}