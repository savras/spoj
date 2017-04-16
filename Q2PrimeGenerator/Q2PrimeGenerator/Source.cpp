#include <iostream>
#include <cmath>
#include <vector>
#include <list>

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::list;

 // Build primes for input range.
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

void useCacheMethod() {
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

void buildSieve(vector<int>& primes) {
	// Init
	int size = primes.size();
	for (size_t i = 2; i < size; i++) {
		if (!primes[i]) {
			continue;
		}

		for (size_t p = i * i; p < size; p += i) {
			primes[p] = false;
		}
	}
}

// https://www.khanacademy.org/computing/computer-science/cryptography/comp-number-theory/v/sieve-of-eratosthenes-prime-adventure-part-4
void useSieveMethod() {
	int t;
	cin >> t;

	int m, n;
	const int size = 31623;	// sqrt(1000000000) + 1	(see video of example for n = 100. We still need to process for i == 11).
	vector<int> primes(size, true);
	buildSieve(primes);

	for (size_t p = 0; p < t; p++) {
		cin >> m >> n;

		for (size_t i = m; i <= n; i++) {
			if (primes[i]) {
				cout << i << endl;
			}
		}
	}	
}

int main() {
	//useCacheMethod();
	useSieveMethod();	
}